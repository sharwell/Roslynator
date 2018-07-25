// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public abstract class DocumentationGenerator
    {
        private SymbolDocumentationModel _emptySymbolDocumentationModel;
        private readonly ImmutableArray<TypeKind> _typeKinds = ImmutableArray.CreateRange(new TypeKind[] { TypeKind.Class, TypeKind.Struct, TypeKind.Interface, TypeKind.Enum, TypeKind.Delegate });

        protected DocumentationGenerator(
            DocumentationModel documentationModel,
            DocumentationUrlProvider urlProvider,
            DocumentationOptions options = null,
            DocumentationResources resources = null)
        {
            DocumentationModel = documentationModel;
            UrlProvider = urlProvider;
            Options = options ?? DocumentationOptions.Default;
            Resources = resources ?? DocumentationResources.Default;
        }

        public DocumentationModel DocumentationModel { get; }

        public DocumentationOptions Options { get; }

        public SymbolDisplayFormatProvider FormatProvider => Options.FormatProvider;

        public DocumentationResources Resources { get; }

        public DocumentationUrlProvider UrlProvider { get; }

        private SymbolDocumentationModel EmptySymbolModel
        {
            get { return _emptySymbolDocumentationModel ?? (_emptySymbolDocumentationModel = SymbolDocumentationModel.Create(DocumentationModel)); }
        }

        private DocumentationWriter CreateWriter(SymbolDocumentationModel symbolModel, SymbolDocumentationModel directoryModel)
        {
            UrlProvider.DirectoryModel = directoryModel;

            DocumentationWriter writer = CreateWriterCore(symbolModel);

            writer.CanCreateMemberLocalUrl = Options.IsEnabled(DocumentationParts.Member);
            writer.CanCreateTypeLocalUrl = Options.IsEnabled(DocumentationParts.Type);

            return writer;
        }

        protected abstract DocumentationWriter CreateWriterCore(SymbolDocumentationModel symbolModel);

        internal SymbolDocumentationModel GetSymbolModel(ISymbol symbol)
        {
            return DocumentationModel.GetSymbolModel(symbol);
        }

        public IEnumerable<DocumentationGeneratorResult> Generate(
            string heading = null,
            string objectModelHeading = null,
            string extendedExternalTypesHeading = null)
        {
            DocumentationParts parts = Options.Parts;

            DocumentationGeneratorResult objectModel = default;
            DocumentationGeneratorResult extendedExternalTypes = default;

            if ((parts & DocumentationParts.ObjectModel) != 0)
            {
                objectModel = GenerateObjectModel(objectModelHeading ?? Resources.ObjectModelTitle);

                if (!objectModel.HasContent)
                    parts &= ~DocumentationParts.ObjectModel;
            }

            if ((parts & DocumentationParts.ExtendedExternalTypes) != 0)
            {
                extendedExternalTypes = GenerateExtendedExternalTypes(extendedExternalTypesHeading ?? Resources.ExtendedExternalTypesTitle);

                if (!extendedExternalTypes.HasContent)
                    parts &= ~DocumentationParts.ExtendedExternalTypes;
            }

            using (DocumentationWriter writer = CreateWriter(symbolModel: EmptySymbolModel, directoryModel: null))
            {
                yield return GenerateRoot(writer, heading, parts);
            }

            bool generateTypes = (parts & DocumentationParts.Type) != 0;

            if ((parts & DocumentationParts.Namespace) != 0)
            {
                foreach (INamespaceSymbol namespaceSymbol in DocumentationModel.Namespaces)
                    yield return GenerateNamespace(namespaceSymbol);
            }

            if (generateTypes)
            {
                bool generateMembers = (parts & DocumentationParts.Member) != 0;

                foreach (INamedTypeSymbol typeSymbol in DocumentationModel.Types)
                {
                    yield return GenerateType(typeSymbol);

                    if (generateMembers)
                    {
                        foreach (DocumentationGeneratorResult result in GenerateMembers(typeSymbol))
                            yield return result;
                    }
                }
            }

            if (objectModel.HasContent)
                yield return objectModel;

            if (extendedExternalTypes.HasContent)
            {
                yield return extendedExternalTypes;

                foreach (ITypeSymbol typeSymbol in DocumentationModel.GetExtendedExternalTypes())
                    yield return GenerateExtendedExternalType(typeSymbol);
            }
        }

        private DocumentationGeneratorResult GenerateRoot(
            DocumentationWriter writer,
            string heading,
            DocumentationParts documentationParts)
        {
            writer.WriteStartDocument();

            if (heading != null)
                writer.WriteHeading1(heading);

            if ((documentationParts & DocumentationParts.ObjectModel) != 0
                && Options.IsEnabled(RootDocumentationParts.ObjectModelLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ObjectModelTitle, WellKnownNames.ObjectModelFileName);
                writer.WriteEndBulletItem();
            }

            if ((documentationParts & DocumentationParts.ExtendedExternalTypes) != 0
                && Options.IsEnabled(RootDocumentationParts.ExtendedExternalTypesLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ExtendedExternalTypesTitle, WellKnownNames.ExtendedExternalTypesFileName);
                writer.WriteEndBulletItem();
            }

            if (Options.IsEnabled(RootDocumentationParts.NamespaceList))
            {
                writer.WriteList(DocumentationModel.Namespaces, Resources.NamespacesTitle, 2, FormatProvider.NamespaceFormat);
            }

            if (Options.IsEnabled(RootDocumentationParts.Namespaces))
            {
                foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in DocumentationModel.Types
                   .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                   .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    writer.WriteHeading(2, grouping.Key, FormatProvider.NamespaceFormat);

                    GenerateNamespaceContent(writer, grouping, 3, addLink: Options.IsEnabled(DocumentationParts.Type));
                }
            }

            writer.WriteEndDocument();

            return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.Root);
        }

        private DocumentationGeneratorResult GenerateNamespace(INamespaceSymbol namespaceSymbol)
        {
            SymbolDocumentationModel symbolModel = GetSymbolModel(namespaceSymbol);

            using (DocumentationWriter writer = CreateWriter(symbolModel: EmptySymbolModel, symbolModel))
            {
                writer.WriteStartDocument();

                if (Options.IsEnabled(NamespaceDocumentationParts.Heading))
                {
                    writer.WriteHeading(1, namespaceSymbol, FormatProvider.NamespaceFormat, addLink: false);
                }

                GenerateNamespaceContent(writer, DocumentationModel.GetTypes(namespaceSymbol), 2, addLink: Options.IsEnabled(DocumentationParts.Type));

                writer.WriteEndDocument();

                return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.Namespace, symbolModel);
            }
        }

        private void GenerateNamespaceContent(
            DocumentationWriter writer,
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel,
            bool addLink)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            foreach (IGrouping<TypeKind, ITypeSymbol> grouping in typeSymbols
                .Where(f => Options.IsNamespacePartEnabled(f.TypeKind))
                .GroupBy(f => f.TypeKind)
                .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), Options.NamespacePartComparer))
            {
                TypeKind typeKind = grouping.Key;

                writer.WriteTable(
                    grouping,
                    Resources.GetPluralName(typeKind),
                    headingLevel,
                    Resources.GetName(typeKind),
                    Resources.SummaryTitle,
                    format,
                    addLink: addLink);
            }
        }

        private DocumentationGeneratorResult GenerateExtendedExternalTypes(string heading = null)
        {
            using (DocumentationWriter writer = CreateWriter(symbolModel: EmptySymbolModel, directoryModel: null))
            {
                writer.WriteStartDocument();

                IEnumerable<INamespaceSymbol> namespaces = DocumentationModel.GetExtendedExternalTypes()
                    .Select(f => f.ContainingNamespace)
                    .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);

                if (namespaces.Any())
                    writer.WriteHeading1(heading);

                writer.WriteList(namespaces, Resources.NamespacesTitle, 2, FormatProvider.NamespaceFormat);

                if (Options.IsEnabled(RootDocumentationParts.Namespaces))
                {
                    foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in DocumentationModel.GetExtendedExternalTypes()
                       .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                       .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                       .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                    {
                        INamespaceSymbol namespaceSymbol = grouping.Key;

                        writer.WriteHeading(2, namespaceSymbol, FormatProvider.NamespaceFormat);

                        foreach (IGrouping<TypeKind, ITypeSymbol> grouping2 in grouping
                            .Where(f => Options.IsNamespacePartEnabled(f.TypeKind))
                            .GroupBy(f => f.TypeKind)
                            .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), Options.NamespacePartComparer))
                        {
                            writer.WriteList(grouping2, Resources.GetPluralName(grouping2.Key), 3, FormatProvider.TypeFormat, canCreateExternalUrl: false);
                        }
                    }
                }

                writer.WriteEndDocument();

                return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.ExtendedExternalTypes);
            }
        }

        private DocumentationGeneratorResult GenerateExtendedExternalType(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationModel symbolModel = GetSymbolModel(typeSymbol);

            using (DocumentationWriter writer = CreateWriter(symbolModel, symbolModel))
            {
                writer.WriteStartDocument();
                writer.WriteStartHeading(1);
                writer.WriteLink(symbolModel, FormatProvider.TitleFormat);
                writer.WriteSpace();
                writer.WriteString(Resources.GetName(typeSymbol.TypeKind));
                writer.WriteSpace();
                writer.WriteString(Resources.ExtensionsTitle);
                writer.WriteEndHeading();

                IEnumerable<IMethodSymbol> extensionMethods = DocumentationModel.GetExtensionMethods(typeSymbol);

                writer.WriteTable(
                    extensionMethods,
                    heading: null,
                    headingLevel: -1,
                    Resources.ExtensionMethodTitle,
                    Resources.SummaryTitle,
                    FormatProvider.MethodFormat);

                writer.WriteEndDocument();

                return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.Type, symbolModel);
            }
        }

        private DocumentationGeneratorResult GenerateType(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;
            bool isDelegateOrEnum = typeKind.Is(TypeKind.Delegate, TypeKind.Enum);

            SymbolDocumentationModel symbolModel = GetSymbolModel(typeSymbol);

            using (DocumentationWriter writer = CreateWriter(symbolModel, symbolModel))
            {
                writer.WriteStartDocument();

                foreach (TypeDocumentationParts part in Options.EnabledAndSortedTypeParts)
                {
                    switch (part)
                    {
                        case TypeDocumentationParts.Title:
                            {
                                writer.WriteTitle(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Namespace:
                            {
                                writer.WriteNamespace(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Assembly:
                            {
                                writer.WriteAssembly(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Obsolete:
                            {
                                if (typeSymbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                                    writer.WriteObsolete(typeSymbol);

                                break;
                            }
                        case TypeDocumentationParts.Summary:
                            {
                                writer.WriteSummary(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Definition:
                            {
                                if (typeSymbol.Kind != SymbolKind.Namespace)
                                    writer.WriteDefinition(typeSymbol);

                                break;
                            }
                        case TypeDocumentationParts.TypeParameters:
                            {
                                writer.WriteTypeParameters(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Parameters:
                            {
                                writer.WriteParameters(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.ReturnValue:
                            {
                                writer.WriteReturnValue(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Inheritance:
                            {
                                writer.WriteInheritance(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Attributes:
                            {
                                writer.WriteAttributes(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Derived:
                            {
                                writer.WriteDerived(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Implements:
                            {
                                writer.WriteImplements(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Examples:
                            {
                                writer.WriteExamples(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Remarks:
                            {
                                writer.WriteRemarks(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Constructors:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteConstructors(symbolModel.GetConstructors());

                                break;
                            }
                        case TypeDocumentationParts.Fields:
                            {
                                if (typeKind != TypeKind.Delegate)
                                {
                                    if (typeKind == TypeKind.Enum)
                                    {
                                        writer.WriteEnumFields(symbolModel.GetFields());
                                    }
                                    else
                                    {
                                        writer.WriteFields(symbolModel.GetFields(includeInherited: true));
                                    }
                                }

                                break;
                            }
                        case TypeDocumentationParts.Properties:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteProperties(symbolModel.GetProperties(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.Methods:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteMethods(symbolModel.GetMethods(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.Operators:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteOperators(symbolModel.GetOperators(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.Events:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteEvents(symbolModel.GetEvents(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.ExplicitInterfaceImplementations:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteExplicitInterfaceImplementations(symbolModel.GetExplicitInterfaceImplementations());

                                break;
                            }
                        case TypeDocumentationParts.ExtensionMethods:
                            {
                                writer.WriteExtensionMethods(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.SeeAlso:
                            {
                                writer.WriteSeeAlso(typeSymbol);
                                break;
                            }
                    }
                }

                writer.WriteEndDocument();

                return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.Type, symbolModel);
            }
        }

        private IEnumerable<DocumentationGeneratorResult> GenerateMembers(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
                yield break;

            SymbolDocumentationModel symbolModel = GetSymbolModel(typeSymbol);

            if (Options.IsEnabled(TypeDocumentationParts.Constructors))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetConstructors()))
                    yield return result;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Fields))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetFields()))
                    yield return result;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Properties))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetProperties()))
                    yield return result;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Methods))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetMethods()))
                    yield return result;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Operators))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetOperators()))
                    yield return result;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Events))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetEvents()))
                    yield return result;
            }

            if (Options.IsEnabled(TypeDocumentationParts.ExplicitInterfaceImplementations))
            {
                foreach (DocumentationGeneratorResult result in GenerateMember(symbolModel.GetExplicitInterfaceImplementations()))
                    yield return result;
            }
        }

        private IEnumerable<DocumentationGeneratorResult> GenerateMember(IEnumerable<ISymbol> members)
        {
            foreach (IGrouping<string, ISymbol> grouping in members.GroupBy(f => f.Name))
            {
                ImmutableArray<ISymbol> symbols = grouping.ToImmutableArray();

                ISymbol symbol = symbols[0];
                SymbolDocumentationModel symbolModel = GetSymbolModel(symbol);

                using (DocumentationWriter writer = CreateWriter(symbolModel, symbolModel))
                {
                    writer.WriteStartDocument();

                    MemberDocumentationWriter memberWriter = MemberDocumentationWriter.Create(symbol, writer);

                    memberWriter.WriteMember(symbol, symbols);

                    writer.WriteEndDocument();

                    yield return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.Member, symbolModel);
                }
            }
        }

        public DocumentationGeneratorResult GenerateObjectModel(string heading)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            using (DocumentationWriter writer = CreateWriter(EmptySymbolModel, null))
            {
                writer.WriteStartDocument();
                writer.WriteHeading1(heading);

                foreach (TypeKind typeKind in _typeKinds.OrderBy(f => f.ToNamespaceDocumentationPart(), Options.NamespacePartComparer))
                {
                    switch (typeKind)
                    {
                        case TypeKind.Class:
                            {
                                if (DocumentationModel.Types.Any(f => !f.IsStatic && f.TypeKind == TypeKind.Class))
                                {
                                    INamedTypeSymbol objectType = DocumentationModel.Compilation.ObjectType;

                                    IEnumerable<INamedTypeSymbol> instanceClasses = DocumentationModel.Types.Where(f => !f.IsStatic && f.TypeKind == TypeKind.Class);

                                    var nodes = new HashSet<ITypeSymbol>(instanceClasses) { objectType };

                                    foreach (INamedTypeSymbol type in instanceClasses)
                                    {
                                        INamedTypeSymbol baseType = type.BaseType;

                                        while (baseType != null)
                                        {
                                            nodes.Add(baseType.OriginalDefinition);
                                            baseType = baseType.BaseType;
                                        }
                                    }

                                    writer.WriteHeading2(Resources.GetPluralName(typeKind));
                                    WriteBulletItem(objectType, nodes, writer);
                                }

                                using (IEnumerator<INamedTypeSymbol> en = DocumentationModel.Types
                                    .Where(f => f.IsStatic && f.TypeKind == TypeKind.Class)
                                    .OrderBy(f => f.ToDisplayString(format)).GetEnumerator())
                                {
                                    if (en.MoveNext())
                                    {
                                        writer.WriteHeading2(Resources.StaticClassesTitle);

                                        do
                                        {
                                            writer.WriteBulletItemLink(en.Current, format);
                                        }
                                        while (en.MoveNext());
                                    }
                                }

                                break;
                            }
                        case TypeKind.Struct:
                        case TypeKind.Interface:
                        case TypeKind.Enum:
                        case TypeKind.Delegate:
                            {
                                using (IEnumerator<INamedTypeSymbol> en = DocumentationModel.Types
                                    .Where(f => f.TypeKind == typeKind)
                                    .OrderBy(f => f.ToDisplayString(format)).GetEnumerator())
                                {
                                    if (en.MoveNext())
                                    {
                                        writer.WriteHeading2(Resources.GetPluralName(typeKind));

                                        do
                                        {
                                            writer.WriteBulletItemLink(en.Current, format);
                                        }
                                        while (en.MoveNext());
                                    }
                                }

                                break;
                            }
                    }
                }

                writer.WriteEndDocument();

                return DocumentationGeneratorResult.Create(writer, UrlProvider, DocumentationKind.ObjectModel);
            }

            void WriteBulletItem(ITypeSymbol baseType, HashSet<ITypeSymbol> nodes, DocumentationWriter writer)
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(baseType, format);

                nodes.Remove(baseType);

                foreach (ITypeSymbol derivedType in nodes
                    .Where(f => f.BaseType?.OriginalDefinition == baseType.OriginalDefinition)
                    .OrderBy(f => f.ToDisplayString(format))
                    .ToList())
                {
                    WriteBulletItem(derivedType, nodes, writer);
                }

                writer.WriteEndBulletItem();
            }
        }
    }
}
