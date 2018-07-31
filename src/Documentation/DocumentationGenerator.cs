// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public abstract class DocumentationGenerator
    {
        private ImmutableArray<TypeDocumentationParts> _enabledAndSortedTypeParts;

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

        public IComparer<NamespaceDocumentationParts> NamespacePartComparer { get; }

        public IComparer<TypeDocumentationParts> TypePartComparer { get; }

        public IComparer<MemberDocumentationParts> MemberPartComparer { get; }

        internal ImmutableArray<TypeDocumentationParts> EnabledAndSortedTypeParts
        {
            get
            {
                if (_enabledAndSortedTypeParts.IsDefault)
                {
                    _enabledAndSortedTypeParts = Enum.GetValues(typeof(TypeDocumentationParts))
                        .Cast<TypeDocumentationParts>()
                        .Where(f => f != TypeDocumentationParts.None
                            && f != TypeDocumentationParts.All
                            && f != TypeDocumentationParts.AllExceptNestedTypes
                            && Options.IsPartEnabled(f))
                        .OrderBy(f => f, TypePartComparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedTypeParts;
            }
        }

        private DocumentationWriter CreateWriter(SymbolDocumentationModel containingModel = null)
        {
            UrlProvider.CurrentFolders = (containingModel != null)
                ? DocumentationModel.GetFolders(containingModel.Symbol)
                : default;

            DocumentationWriter writer = CreateWriterCore();

            writer.CanCreateMemberLocalUrl = Options.IsPartEnabled(DocumentationParts.Member);
            writer.CanCreateTypeLocalUrl = Options.IsPartEnabled(DocumentationParts.Type);

            return writer;
        }

        protected abstract DocumentationWriter CreateWriterCore();

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

            using (DocumentationWriter writer = CreateWriter())
            {
                yield return GenerateRoot(writer, heading, parts);
            }

            bool generateTypes = (parts & DocumentationParts.Type) != 0;

            if ((parts & DocumentationParts.Namespace) != 0)
            {
                foreach (NamespaceDocumentationModel namespaceModel in DocumentationModel.Namespaces)
                    yield return GenerateNamespace(namespaceModel);
            }

            if (generateTypes)
            {
                bool generateMembers = (parts & DocumentationParts.Member) != 0;

                foreach (TypeDocumentationModel typeModel in DocumentationModel.Types)
                {
                    yield return GenerateType(typeModel);

                    if (generateMembers)
                    {
                        foreach (DocumentationGeneratorResult result in GenerateMembers(typeModel))
                            yield return result;
                    }
                }
            }

            if (objectModel.HasContent)
                yield return objectModel;

            if (extendedExternalTypes.HasContent)
            {
                yield return extendedExternalTypes;

                foreach (TypeDocumentationModel typeModel in DocumentationModel.GetExtendedExternalTypes())
                    yield return GenerateExtendedExternalType(typeModel);
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
                && Options.IsPartEnabled(RootDocumentationParts.ObjectModelLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ObjectModelTitle, WellKnownNames.ObjectModelFileName);
                writer.WriteEndBulletItem();
            }

            if ((documentationParts & DocumentationParts.ExtendedExternalTypes) != 0
                && Options.IsPartEnabled(RootDocumentationParts.ExtendedExternalTypesLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ExtendedExternalTypesTitle, WellKnownNames.ExtendedExternalTypesFileName);
                writer.WriteEndBulletItem();
            }

            if (Options.IsPartEnabled(RootDocumentationParts.NamespaceList))
            {
                writer.WriteList(DocumentationModel.Namespaces.Select(f => f.Symbol), Resources.NamespacesTitle, 2, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);
            }

            if (Options.IsPartEnabled(RootDocumentationParts.Namespaces))
            {
                foreach (NamespaceDocumentationModel namespaceModel in DocumentationModel.Namespaces
                   .OrderBy(f => f.Symbol.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces)))
                {
                    writer.WriteHeading(2, namespaceModel.Symbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);

                    GenerateNamespaceContent(writer, namespaceModel, 3, addLink: Options.IsPartEnabled(DocumentationParts.Type));
                }
            }

            writer.WriteEndDocument();

            return CreateResult(writer, UrlProvider, DocumentationKind.Root);
        }

        private DocumentationGeneratorResult GenerateNamespace(NamespaceDocumentationModel namespaceModel)
        {
            using (DocumentationWriter writer = CreateWriter(namespaceModel))
            {
                writer.WriteStartDocument();

                if (Options.IsPartEnabled(NamespaceDocumentationParts.Heading))
                    writer.WriteHeading(1, namespaceModel.Symbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces, addLink: false);

                GenerateNamespaceContent(writer, namespaceModel, 2, addLink: Options.IsPartEnabled(DocumentationParts.Type));

                writer.WriteEndDocument();

                return CreateResult(writer, UrlProvider, DocumentationKind.Namespace, namespaceModel.Symbol);
            }
        }

        private void GenerateNamespaceContent(
            DocumentationWriter writer,
            NamespaceDocumentationModel namespaceModel,
            int headingLevel,
            bool addLink)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            foreach (IGrouping<TypeKind, INamedTypeSymbol> grouping in namespaceModel.Types
                .Select(f => f.TypeSymbol)
                .Where(f => Options.IsNamespacePartEnabled(f.TypeKind))
                .GroupBy(f => f.TypeKind)
                .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), NamespacePartComparer ?? NamespaceDocumentationPartComparer.Instance))
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
            IEnumerable<TypeDocumentationModel> extendedExternalTypes = DocumentationModel.GetExtendedExternalTypes();

            IEnumerable<INamespaceSymbol> namespaces = extendedExternalTypes
                .Select(f => f.Symbol.ContainingNamespace)
                .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);

            if (!namespaces.Any())
                return CreateResult(null, UrlProvider, DocumentationKind.ExtendedExternalTypes);

            using (DocumentationWriter writer = CreateWriter())
            {
                writer.WriteStartDocument();
                writer.WriteHeading1(heading);
                writer.WriteList(namespaces, Resources.NamespacesTitle, 2, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);

                if (Options.IsPartEnabled(RootDocumentationParts.Namespaces))
                {
                    foreach (IGrouping<INamespaceSymbol, TypeDocumentationModel> typesByNamespaces in extendedExternalTypes
                        .OrderBy(f => f.Symbol.ToDisplayString(FormatProvider.TypeFormat))
                        .GroupBy(f => f.Symbol.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                        .OrderBy(f => f.Key.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces)))
                    {
                        INamespaceSymbol namespaceSymbol = typesByNamespaces.Key;

                        writer.WriteHeading(2, namespaceSymbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);

                        foreach (IGrouping<TypeKind, TypeDocumentationModel> typesByKind in typesByNamespaces
                            .Where(f => Options.IsNamespacePartEnabled(f.TypeKind))
                            .GroupBy(f => f.TypeKind)
                            .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), NamespacePartComparer))
                        {
                            writer.WriteList(typesByKind.Select(f => f.Symbol), Resources.GetPluralName(typesByKind.Key), 3, FormatProvider.TypeFormat, canCreateExternalUrl: false);
                        }
                    }
                }

                writer.WriteEndDocument();

                return CreateResult(writer, UrlProvider, DocumentationKind.ExtendedExternalTypes);
            }
        }

        private DocumentationGeneratorResult GenerateExtendedExternalType(TypeDocumentationModel typeModel)
        {
            using (DocumentationWriter writer = CreateWriter(typeModel))
            {
                writer.WriteStartDocument();
                writer.WriteStartHeading(1);
                writer.WriteLink(typeModel.Symbol, FormatProvider.TitleFormat);
                writer.WriteSpace();
                writer.WriteString(Resources.GetName(typeModel.TypeKind));
                writer.WriteSpace();
                writer.WriteString(Resources.ExtensionsTitle);
                writer.WriteEndHeading();

                writer.WriteTable(
                    typeModel.GetExtensionMethods(),
                    heading: null,
                    headingLevel: -1,
                    Resources.ExtensionMethodTitle,
                    Resources.SummaryTitle,
                    FormatProvider.SimpleDefinitionFormat);

                writer.WriteEndDocument();

                return CreateResult(writer, UrlProvider, DocumentationKind.Type, typeModel.Symbol);
            }
        }

        private DocumentationGeneratorResult GenerateType(TypeDocumentationModel typeModel)
        {
            INamedTypeSymbol typeSymbol = typeModel.TypeSymbol;

            ImmutableArray<INamedTypeSymbol> nestedTypes = default;

            using (DocumentationWriter writer = CreateWriter(typeModel))
            {
                writer.WriteStartDocument();

                foreach (TypeDocumentationParts part in EnabledAndSortedTypeParts)
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
                                if (typeModel.IsObsolete)
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
                                writer.WriteDerived(typeModel.GetDerivedTypes());
                                break;
                            }
                        case TypeDocumentationParts.Implements:
                            {
                                writer.WriteImplements(typeModel.GetImplementedTypes());
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
                                writer.WriteConstructors(typeModel.GetConstructors());
                                break;
                            }
                        case TypeDocumentationParts.Fields:
                            {
                                if (typeModel.TypeKind == TypeKind.Enum)
                                {
                                    writer.WriteEnumFields(typeModel.GetFields());
                                }
                                else
                                {
                                    writer.WriteFields(typeModel.GetFields(includeInherited: true), containingType: typeSymbol);
                                }

                                break;
                            }
                        case TypeDocumentationParts.Properties:
                            {
                                writer.WriteProperties(typeModel.GetProperties(includeInherited: true), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Methods:
                            {
                                writer.WriteMethods(typeModel.GetMethods(includeInherited: true), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Operators:
                            {
                                writer.WriteOperators(typeModel.GetOperators(includeInherited: true), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Events:
                            {
                                writer.WriteEvents(typeModel.GetEvents(includeInherited: true), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.ExplicitInterfaceImplementations:
                            {
                                writer.WriteExplicitInterfaceImplementations(typeModel.GetExplicitInterfaceImplementations());
                                break;
                            }
                        case TypeDocumentationParts.ExtensionMethods:
                            {
                                writer.WriteExtensionMethods(typeModel.GetExtensionMethods());
                                break;
                            }
                        case TypeDocumentationParts.Classes:
                            {
                                if (nestedTypes.IsDefault)
                                    nestedTypes = typeSymbol.GetTypeMembers();

                                WriteTypes(writer, nestedTypes, TypeKind.Class);
                                break;
                            }
                        case TypeDocumentationParts.Structs:
                            {
                                if (nestedTypes.IsDefault)
                                    nestedTypes = typeSymbol.GetTypeMembers();

                                WriteTypes(writer, nestedTypes, TypeKind.Struct);
                                break;
                            }
                        case TypeDocumentationParts.Interfaces:
                            {
                                if (nestedTypes.IsDefault)
                                    nestedTypes = typeSymbol.GetTypeMembers();

                                WriteTypes(writer, nestedTypes, TypeKind.Interface);
                                break;
                            }
                        case TypeDocumentationParts.Enums:
                            {
                                if (nestedTypes.IsDefault)
                                    nestedTypes = typeSymbol.GetTypeMembers();

                                WriteTypes(writer, nestedTypes, TypeKind.Enum);
                                break;
                            }
                        case TypeDocumentationParts.Delegates:
                            {
                                if (nestedTypes.IsDefault)
                                    nestedTypes = typeSymbol.GetTypeMembers();

                                WriteTypes(writer, nestedTypes, TypeKind.Delegate);
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

                return CreateResult(writer, UrlProvider, DocumentationKind.Type, typeSymbol);
            }

            void WriteTypes(
                DocumentationWriter writer,
                IEnumerable<INamedTypeSymbol> types,
                TypeKind typeKind)
            {
                writer.WriteTable(
                    types.Where(f => f.TypeKind == typeKind && DocumentationModel.IsVisible(f)),
                    Resources.GetPluralName(typeKind),
                    headingLevel: 2,
                    Resources.GetName(typeKind),
                    Resources.SummaryTitle,
                    FormatProvider.TypeFormat);
            }
        }

        private IEnumerable<DocumentationGeneratorResult> GenerateMembers(TypeDocumentationModel typeModel)
        {
            if (!typeModel.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
            {
                foreach (MemberDocumentationModel model in typeModel.GetMembers(Options.TypeParts))
                {
                    using (DocumentationWriter writer = CreateWriter(model))
                    {
                        writer.WriteStartDocument();

                        MemberDocumentationWriter memberWriter = MemberDocumentationWriter.Create(model.Symbol, writer);

                        memberWriter.WriteMember(model);

                        writer.WriteEndDocument();

                        yield return CreateResult(writer, UrlProvider, DocumentationKind.Member, model.Symbol);
                    }
                }
            }
        }

        public DocumentationGeneratorResult GenerateObjectModel(string heading = null)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            using (DocumentationWriter writer = CreateWriter())
            {
                writer.WriteStartDocument();

                if (!string.IsNullOrEmpty(heading))
                    writer.WriteHeading1(heading);

                IEnumerable<INamedTypeSymbol> typeSymbols = DocumentationModel.Types.Select(f => f.TypeSymbol);

                foreach (TypeKind typeKind in _typeKinds.OrderBy(f => f.ToNamespaceDocumentationPart(), NamespacePartComparer))
                {
                    switch (typeKind)
                    {
                        case TypeKind.Class:
                            {
                                if (typeSymbols.Any(f => !f.IsStatic && f.TypeKind == TypeKind.Class))
                                {
                                    INamedTypeSymbol objectType = DocumentationModel.Compilation.ObjectType;

                                    IEnumerable<INamedTypeSymbol> instanceClasses = typeSymbols.Where(f => !f.IsStatic && f.TypeKind == TypeKind.Class);

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

                                using (IEnumerator<INamedTypeSymbol> en = typeSymbols
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
                                using (IEnumerator<INamedTypeSymbol> en = typeSymbols
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

                return CreateResult(writer, UrlProvider, DocumentationKind.ObjectModel);
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

        private DocumentationGeneratorResult CreateResult(DocumentationWriter writer, DocumentationUrlProvider urlProvider, DocumentationKind kind, ISymbol symbol = null)
        {
            ImmutableArray<string> folders = (symbol != null) ? DocumentationModel.GetFolders(symbol) : default;

            return new DocumentationGeneratorResult(
                writer?.ToString(),
                urlProvider.GetDocumentPath(kind, folders), kind);
        }
    }
}
