// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    //TODO: ExternalNamespaces, ExternalTypes
    public class DocumentationModel
    {
        private ImmutableArray<NamespaceDocumentationModel> _namespaceModels;

        private ImmutableArray<TypeDocumentationModel> _typeModels;

        private readonly Dictionary<ISymbol, SymbolDocumentationModel> _symbolDocumentationModels;

        private readonly Dictionary<ISymbol, string> _commentIds;

        private readonly Dictionary<ISymbol, ImmutableArray<string>> _symbolFolders;

        private Dictionary<IAssemblySymbol, XmlDocumentation> _xmlDocumentations;

        public DocumentationModel(Compilation compilation, IEnumerable<IAssemblySymbol> assemblies)
        {
            Compilation = compilation;
            Assemblies = ImmutableArray.CreateRange(assemblies);

            _symbolDocumentationModels = new Dictionary<ISymbol, SymbolDocumentationModel>();
            _commentIds = new Dictionary<ISymbol, string>();
            _symbolFolders = new Dictionary<ISymbol, ImmutableArray<string>>();
        }

        public Compilation Compilation { get; }

        public ImmutableArray<IAssemblySymbol> Assemblies { get; }

        public string Language => Compilation.Language;

        public IEnumerable<MetadataReference> References => Compilation.References;

        public IEnumerable<NamespaceDocumentationModel> Namespaces
        {
            get
            {
                if (_namespaceModels.IsDefault)
                {
                    _namespaceModels = Types
                        .Select(f => f.Symbol.ContainingNamespace)
                        .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                        .Select(f => GetNamespaceModel(f))
                        .ToImmutableArray();
                }

                return _namespaceModels;
            }
        }

        public ImmutableArray<TypeDocumentationModel> Types
        {
            get
            {
                if (_typeModels.IsDefault)
                {
                    _typeModels = Assemblies
                        .SelectMany(f => f.GetTypes(typeSymbol => IsVisible(typeSymbol)))
                        .Select(f => GetTypeModel(f))
                        .ToImmutableArray();
                }

                return _typeModels;
            }
        }

        public virtual bool IsVisible(ISymbol symbol)
        {
            return symbol.IsPubliclyVisible();
        }

        public IEnumerable<IMethodSymbol> GetExtensionMethods()
        {
            foreach (TypeDocumentationModel typeModel in Types)
            {
                if (typeModel.TypeSymbol.MightContainExtensionMethods)
                {
                    foreach (ISymbol member in typeModel.Members)
                    {
                        if (member.Kind == SymbolKind.Method
                            && member.IsStatic)
                        {
                            var methodSymbol = (IMethodSymbol)member;

                            if (methodSymbol.IsExtensionMethod)
                                yield return methodSymbol;
                        }
                    }
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetExtensionMethods(INamedTypeSymbol typeSymbol)
        {
            foreach (TypeDocumentationModel typeModel in Types)
            {
                if (typeModel.TypeSymbol.MightContainExtensionMethods)
                {
                    foreach (ISymbol member in typeModel.Members)
                    {
                        if (member.Kind == SymbolKind.Method
                            && member.IsStatic)
                        {
                            var methodSymbol = (IMethodSymbol)member;

                            if (methodSymbol.IsExtensionMethod)
                            {
                                ITypeSymbol typeSymbol2 = GetExtendedType(methodSymbol);

                                if (typeSymbol == typeSymbol2)
                                    yield return methodSymbol;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<TypeDocumentationModel> GetExtendedExternalTypes()
        {
            return Iterator().Distinct().Select(f => GetTypeModel(f));

            IEnumerable<INamedTypeSymbol> Iterator()
            {
                foreach (IMethodSymbol methodSymbol in GetExtensionMethods())
                {
                    INamedTypeSymbol typeSymbol = GetExternalSymbol(methodSymbol);

                    if (typeSymbol != null)
                        yield return typeSymbol;
                }
            }

            INamedTypeSymbol GetExternalSymbol(IMethodSymbol methodSymbol)
            {
                INamedTypeSymbol type = GetExtendedType(methodSymbol);

                if (type == null)
                    return null;

                foreach (IAssemblySymbol assembly in Assemblies)
                {
                    if (type.ContainingAssembly == assembly)
                        return null;
                }

                return type;
            }
        }

        private static INamedTypeSymbol GetExtendedType(IMethodSymbol methodSymbol)
        {
            ITypeSymbol type = methodSymbol.Parameters[0].Type.OriginalDefinition;

            switch (type.Kind)
            {
                case SymbolKind.NamedType:
                    return (INamedTypeSymbol)type;
                case SymbolKind.TypeParameter:
                    return GetTypeParameterConstraintClass((ITypeParameterSymbol)type);
            }

            return null;

            INamedTypeSymbol GetTypeParameterConstraintClass(ITypeParameterSymbol typeParameter)
            {
                foreach (ITypeSymbol constraintType in typeParameter.ConstraintTypes)
                {
                    if (constraintType.TypeKind == TypeKind.Class)
                    {
                        return (INamedTypeSymbol)constraintType;
                    }
                    else if (constraintType.TypeKind == TypeKind.TypeParameter)
                    {
                        return GetTypeParameterConstraintClass((ITypeParameterSymbol)constraintType);
                    }
                }

                return null;
            }
        }

        public bool IsExternal(ISymbol symbol)
        {
            foreach (IAssemblySymbol assembly in Assemblies)
            {
                if (symbol.ContainingAssembly == assembly)
                    return false;
            }

            return true;
        }

        public NamespaceDocumentationModel GetNamespaceModel(INamespaceSymbol namespaceSymbol)
        {
            if (_symbolDocumentationModels.TryGetValue(namespaceSymbol, out SymbolDocumentationModel model))
                return (NamespaceDocumentationModel)model;

            NamespaceDocumentationModel namespaceModel = NamespaceDocumentationModel.Create(namespaceSymbol, this);

            _symbolDocumentationModels[namespaceSymbol] = namespaceModel;

            return namespaceModel;
        }

        public TypeDocumentationModel GetTypeModel(INamedTypeSymbol typeSymbol)
        {
            if (_symbolDocumentationModels.TryGetValue(typeSymbol, out SymbolDocumentationModel model))
                return (TypeDocumentationModel)model;

            TypeDocumentationModel typeModel = TypeDocumentationModel.Create(typeSymbol, this);

            _symbolDocumentationModels[typeSymbol] = typeModel;

            return typeModel;
        }

        public ImmutableArray<string> GetFolders(ISymbol symbol)
        {
            if (_symbolFolders.TryGetValue(symbol, out ImmutableArray<string> folders))
                return folders;

            ImmutableArray<string>.Builder builder = ImmutableArray.CreateBuilder<string>();

            if (symbol.Kind == SymbolKind.Namespace
                && ((INamespaceSymbol)symbol).IsGlobalNamespace)
            {
                builder.Add(WellKnownNames.GlobalNamespaceName);
            }
            else if (symbol.Kind == SymbolKind.Method
                && ((IMethodSymbol)symbol).MethodKind == MethodKind.Constructor)
            {
                builder.Add(WellKnownNames.ConstructorName);
            }
            else if (symbol.Kind == SymbolKind.Property
                && ((IPropertySymbol)symbol).IsIndexer)
            {
                builder.Add("Item");
            }
            else
            {
                ISymbol explicitImplementation = symbol.GetFirstExplicitInterfaceImplementation();

                if (explicitImplementation != null)
                {
                    string name = explicitImplementation
                        .ToDisplayParts(SymbolDisplayFormats.ExplicitImplementationFullName, SymbolDisplayAdditionalMemberOptions.UseItemPropertyName)
                        .Where(part => part.Kind != SymbolDisplayPartKind.Space)
                        .Select(part => (part.IsPunctuation()) ? part.WithText("-") : part)
                        .ToImmutableArray()
                        .ToDisplayString();

                    builder.Add(name);
                }
                else
                {
                    int arity = symbol.GetArity();

                    if (arity > 0)
                    {
                        builder.Add(symbol.Name + "-" + arity.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        builder.Add(symbol.Name);
                    }
                }
            }

            INamedTypeSymbol containingType = symbol.ContainingType;

            while (containingType != null)
            {
                int arity = containingType.Arity;

                builder.Add((arity > 0) ? containingType.Name + "-" + arity.ToString(CultureInfo.InvariantCulture) : containingType.Name);

                containingType = containingType.ContainingType;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            if (containingNamespace != null)
            {
                if (containingNamespace.IsGlobalNamespace)
                {
                    if (symbol.Kind != SymbolKind.Namespace)
                    {
                        builder.Add(WellKnownNames.GlobalNamespaceName);
                    }
                }
                else
                {
                    do
                    {
                        builder.Add(containingNamespace.Name);

                        containingNamespace = containingNamespace.ContainingNamespace;
                    }
                    while (containingNamespace?.IsGlobalNamespace == false);
                }
            }

            builder.Reverse();

            ImmutableArray<string> names = builder.ToImmutableArray();

            _symbolFolders[symbol] = names;

            return names;
        }

        internal ISymbol GetFirstSymbolForDeclarationId(string id)
        {
            return DocumentationCommentId.GetFirstSymbolForDeclarationId(id, Compilation);
        }

        internal ISymbol GetFirstSymbolForReferenceId(string id)
        {
            return DocumentationCommentId.GetFirstSymbolForReferenceId(id, Compilation);
        }

        private XmlDocumentation GetXmlDocumentation(IAssemblySymbol assemblySymbol)
        {
            if (_xmlDocumentations == null)
                _xmlDocumentations = new Dictionary<IAssemblySymbol, XmlDocumentation>();

            if (!_xmlDocumentations.TryGetValue(assemblySymbol, out XmlDocumentation xmlDocumentation))
            {
                if (Assemblies.Contains(assemblySymbol))
                {
                    var reference = Compilation.GetMetadataReference(assemblySymbol) as PortableExecutableReference;

                    string xmlDocPath = Path.ChangeExtension(reference.FilePath, "xml");

                    if (File.Exists(xmlDocPath))
                        xmlDocumentation = XmlDocumentation.Load(xmlDocPath);
                }

                _xmlDocumentations[assemblySymbol] = xmlDocumentation;
            }

            return xmlDocumentation;
        }

        internal SymbolXmlDocumentation GetXmlDocumentation(ISymbol symbol)
        {
            if (!_commentIds.TryGetValue(symbol, out string commentId))
            {
                commentId = symbol.GetDocumentationCommentId();

                _commentIds[symbol] = commentId;
            }

            if (commentId == null)
                return null;

            return GetXmlDocumentation(symbol.ContainingAssembly)?.GetDocumentation(commentId);
        }
    }
}
