// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationModel
    {
        private ImmutableArray<NamespaceDocumentationModel> _namespaceModels;

        private ImmutableArray<TypeDocumentationModel> _typeModels;

        private readonly Dictionary<ISymbol, SymbolDocumentationModel> _symbolDocumentationModels;

        private Dictionary<IAssemblySymbol, XmlDocumentation> _xmlDocumentations;

        public DocumentationModel(Compilation compilation, IEnumerable<IAssemblySymbol> assemblies)
        {
            Compilation = compilation;
            Assemblies = ImmutableArray.CreateRange(assemblies);

            _symbolDocumentationModels = new Dictionary<ISymbol, SymbolDocumentationModel>();
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

        public IEnumerable<TypeDocumentationModel> ExtendedExternalTypes()
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
                INamedTypeSymbol type = GetExtendedTypeSymbol(methodSymbol);

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

        public static INamedTypeSymbol GetExtendedTypeSymbol(IMethodSymbol methodSymbol)
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

        //TODO: GetSymbolModel
        internal SymbolDocumentationModel GetSymbolModel(ISymbol symbol)
        {
            if (_symbolDocumentationModels.TryGetValue(symbol, out SymbolDocumentationModel model))
                return model;

            switch (symbol.Kind)
            {
                case SymbolKind.Namespace:
                    {
                        model = NamespaceDocumentationModel.Create((INamespaceSymbol)symbol, this);
                        break;
                    }
                case SymbolKind.NamedType:
                    {
                        model = TypeDocumentationModel.Create((INamedTypeSymbol)symbol, this);
                        break;
                    }
                case SymbolKind.Event:
                case SymbolKind.Field:
                case SymbolKind.Method:
                case SymbolKind.Property:
                    {
                        model = MemberDocumentationModel.Create(symbol, default, this);
                        break;
                    }
                case SymbolKind.Parameter:
                case SymbolKind.TypeParameter:
                    {
                        model = new SymbolDocumentationModel(symbol, this);
                        break;
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }

            _symbolDocumentationModels[symbol] = model;

            return model;
        }

        internal ISymbol GetFirstSymbolForDeclarationId(string id)
        {
            return DocumentationCommentId.GetFirstSymbolForDeclarationId(id, Compilation);
        }

        internal ISymbol GetFirstSymbolForReferenceId(string id)
        {
            return DocumentationCommentId.GetFirstSymbolForReferenceId(id, Compilation);
        }

        internal XmlDocumentation GetXmlDocumentation(IAssemblySymbol assemblySymbol)
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

        internal SymbolXmlDocumentation GetSymbolDocumentation(ISymbol symbol)
        {
            return GetXmlDocumentation(symbol.ContainingAssembly)?.GetDocumentation(GetSymbolModel(symbol).CommentId);
        }
    }
}
