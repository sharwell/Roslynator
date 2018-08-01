// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public abstract class DocumentationGenerator
    {
        private ImmutableArray<TypeDocumentationParts> _enabledAndSortedTypeParts;
        private ImmutableArray<NamespaceDocumentationParts> _enabledAndSortedNamespaceParts;

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

        internal ImmutableArray<NamespaceDocumentationParts> EnabledAndSortedNamespaceParts
        {
            get
            {
                if (_enabledAndSortedNamespaceParts.IsDefault)
                {
                    _enabledAndSortedNamespaceParts = Enum.GetValues(typeof(NamespaceDocumentationParts))
                        .Cast<NamespaceDocumentationParts>()
                        .Where(f => f != NamespaceDocumentationParts.None
                            && f != NamespaceDocumentationParts.All
                            && Options.IsPartEnabled(f))
                        .OrderBy(f => f, NamespacePartComparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedNamespaceParts;
            }
        }

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

        private DocumentationWriter CreateWriter(ISymbol currentSymbol = null)
        {
            UrlProvider.CurrentSymbol = currentSymbol;

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
            DocumentationParts parts = Options.DocumentationParts;

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
                foreach (NamespaceDocumentationModel namespaceModel in DocumentationModel.NamespaceModels)
                    yield return GenerateNamespace(namespaceModel);
            }

            if (generateTypes)
            {
                bool generateMembers = (parts & DocumentationParts.Member) != 0;

                foreach (TypeDocumentationModel typeModel in DocumentationModel.TypeModels)
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

                foreach (INamedTypeSymbol typeSymbol in DocumentationModel.GetExtendedExternalTypes())
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
                writer.WriteList(DocumentationModel.NamespaceModels.Select(f => f.Symbol), Resources.NamespacesTitle, 2, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);
            }

            if (Options.IsPartEnabled(RootDocumentationParts.Namespaces))
            {
                foreach (NamespaceDocumentationModel namespaceModel in DocumentationModel.NamespaceModels
                   .OrderBy(f => f.Symbol.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces)))
                {
                    GenerateNamespaceContent(writer, namespaceModel, headingLevelOffset: 1, disabledParts: NamespaceDocumentationParts.Examples | NamespaceDocumentationParts.Remarks | NamespaceDocumentationParts.SeeAlso);
                }
            }

            writer.WriteEndDocument();

            return CreateResult(writer, DocumentationKind.Root);
        }

        private DocumentationGeneratorResult GenerateNamespace(NamespaceDocumentationModel namespaceModel)
        {
            using (DocumentationWriter writer = CreateWriter(namespaceModel.Symbol))
            {
                writer.WriteStartDocument();

                GenerateNamespaceContent(writer, namespaceModel, addLink: false);

                writer.WriteEndDocument();

                return CreateResult(writer, DocumentationKind.Namespace, namespaceModel.Symbol);
            }
        }

        private void GenerateNamespaceContent(DocumentationWriter writer,
            NamespaceDocumentationModel namespaceModel,
            int headingLevelOffset = 0,
            bool addLink = true,
            NamespaceDocumentationParts disabledParts = NamespaceDocumentationParts.None)
        {
            foreach (NamespaceDocumentationParts part in EnabledAndSortedNamespaceParts)
            {
                if ((disabledParts & part) != 0)
                    continue;

                switch (part)
                {
                    case NamespaceDocumentationParts.Heading:
                        {
                            writer.WriteHeading(1 + headingLevelOffset, namespaceModel.Symbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces, addLink: addLink);
                            break;
                        }
                    case NamespaceDocumentationParts.Summary:
                        {
                            DocumentationModel.GetXmlDocumentation(namespaceModel.Symbol, Options.PreferredCultureName)?.WriteElementContentTo(writer, WellKnownTags.Summary);
                            break;
                        }
                    case NamespaceDocumentationParts.Examples:
                        {
                            writer.WriteExamples(namespaceModel.Symbol);
                            break;
                        }
                    case NamespaceDocumentationParts.Remarks:
                        {
                            writer.WriteRemarks(namespaceModel.Symbol);
                            break;
                        }
                    case NamespaceDocumentationParts.Classes:
                        {
                            WriteTypes(namespaceModel.TypeModels, TypeKind.Class);
                            break;
                        }
                    case NamespaceDocumentationParts.Structs:
                        {
                            WriteTypes(namespaceModel.TypeModels, TypeKind.Struct);
                            break;
                        }
                    case NamespaceDocumentationParts.Interfaces:
                        {
                            WriteTypes(namespaceModel.TypeModels, TypeKind.Interface);
                            break;
                        }
                    case NamespaceDocumentationParts.Enums:
                        {
                            WriteTypes(namespaceModel.TypeModels, TypeKind.Enum);
                            break;
                        }
                    case NamespaceDocumentationParts.Delegates:
                        {
                            WriteTypes(namespaceModel.TypeModels, TypeKind.Delegate);
                            break;
                        }
                    case NamespaceDocumentationParts.SeeAlso:
                        {
                            writer.WriteSeeAlso(namespaceModel.Symbol);
                            break;
                        }
                    default:
                        {
                            throw new InvalidOperationException();
                        }
                }
            }

            void WriteTypes(
                IEnumerable<TypeDocumentationModel> types,
                TypeKind typeKind)
            {
                writer.WriteTable(
                    types.Where(f => f.TypeKind == typeKind).Select(f => f.TypeSymbol),
                    Resources.GetPluralName(typeKind),
                    headingLevel: 2 + headingLevelOffset,
                    Resources.GetName(typeKind),
                    Resources.SummaryTitle,
                    FormatProvider.TypeFormat,
                    addLink: Options.IsPartEnabled(DocumentationParts.Type));
            }
        }

        private DocumentationGeneratorResult GenerateExtendedExternalTypes(string heading = null)
        {
            IEnumerable<INamedTypeSymbol> extendedExternalTypes = DocumentationModel.GetExtendedExternalTypes();

            IEnumerable<INamespaceSymbol> namespaces = extendedExternalTypes
                .Select(f => f.ContainingNamespace)
                .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);

            if (!namespaces.Any())
                return CreateResult(null, DocumentationKind.ExtendedExternalTypes);

            using (DocumentationWriter writer = CreateWriter())
            {
                writer.WriteStartDocument();
                writer.WriteHeading1(heading);
                writer.WriteList(namespaces, Resources.NamespacesTitle, 2, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);

                if (Options.IsPartEnabled(RootDocumentationParts.Namespaces))
                {
                    foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> typesByNamespaces in extendedExternalTypes
                        .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                        .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                        .OrderBy(f => f.Key.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces)))
                    {
                        INamespaceSymbol namespaceSymbol = typesByNamespaces.Key;

                        writer.WriteHeading(2, namespaceSymbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);

                        foreach (IGrouping<TypeKind, INamedTypeSymbol> typesByKind in typesByNamespaces
                            .Where(f => IsNamespacePartEnabled(f.TypeKind))
                            .GroupBy(f => f.TypeKind)
                            .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), NamespacePartComparer))
                        {
                            writer.WriteList(typesByKind, Resources.GetPluralName(typesByKind.Key), 3, FormatProvider.TypeFormat, canCreateExternalUrl: false);
                        }
                    }
                }

                writer.WriteEndDocument();

                return CreateResult(writer, DocumentationKind.ExtendedExternalTypes);
            }

            bool IsNamespacePartEnabled(TypeKind typeKind)
            {
                switch (typeKind)
                {
                    case TypeKind.Class:
                        return Options.IsPartEnabled(NamespaceDocumentationParts.Classes);
                    case TypeKind.Delegate:
                        return Options.IsPartEnabled(NamespaceDocumentationParts.Delegates);
                    case TypeKind.Enum:
                        return Options.IsPartEnabled(NamespaceDocumentationParts.Enums);
                    case TypeKind.Interface:
                        return Options.IsPartEnabled(NamespaceDocumentationParts.Interfaces);
                    case TypeKind.Struct:
                        return Options.IsPartEnabled(NamespaceDocumentationParts.Structs);
                    default:
                        return false;
                }
            }
        }

        private DocumentationGeneratorResult GenerateExtendedExternalType(INamedTypeSymbol typeSymbol)
        {
            using (DocumentationWriter writer = CreateWriter(typeSymbol))
            {
                writer.WriteStartDocument();
                writer.WriteStartHeading(1);
                writer.WriteLink(typeSymbol, FormatProvider.TitleFormat);
                writer.WriteSpace();
                writer.WriteString(Resources.GetName(typeSymbol.TypeKind));
                writer.WriteSpace();
                writer.WriteString(Resources.ExtensionsTitle);
                writer.WriteEndHeading();

                writer.WriteTable(
                    DocumentationModel.GetExtensionMethods(typeSymbol),
                    heading: null,
                    headingLevel: -1,
                    Resources.ExtensionMethodTitle,
                    Resources.SummaryTitle,
                    FormatProvider.SimpleDefinitionFormat);

                writer.WriteEndDocument();

                return CreateResult(writer, DocumentationKind.Type, typeSymbol);
            }
        }

        private DocumentationGeneratorResult GenerateType(TypeDocumentationModel typeModel)
        {
            INamedTypeSymbol typeSymbol = typeModel.TypeSymbol;

            ImmutableArray<INamedTypeSymbol> nestedTypes = default;

            using (DocumentationWriter writer = CreateWriter(typeSymbol))
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
                                    writer.WriteObsoleteMessage(typeSymbol);

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
                                writer.WriteDerivedTypes(typeModel.GetDerivedTypes());
                                break;
                            }
                        case TypeDocumentationParts.Implements:
                            {
                                writer.WriteImplementedInterfaces(typeModel.GetImplementedInterfaces(Options.OmitIEnumerable));
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
                                    writer.WriteEnumFields(typeModel.GetFields(), typeSymbol);
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

                return CreateResult(writer, DocumentationKind.Type, typeSymbol);
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
                    SymbolDisplayFormats.TypeNameAndTypeParameters);
            }
        }

        private IEnumerable<DocumentationGeneratorResult> GenerateMembers(TypeDocumentationModel typeModel)
        {
            if (!typeModel.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
            {
                foreach (MemberDocumentationModel model in typeModel.GetMemberModels(Options.TypeParts))
                {
                    using (DocumentationWriter writer = CreateWriter(model.Symbol))
                    {
                        writer.WriteStartDocument();

                        MemberDocumentationWriter memberWriter = MemberDocumentationWriter.Create(model.Symbol, writer);

                        memberWriter.WriteMember(model);

                        writer.WriteEndDocument();

                        yield return CreateResult(writer, DocumentationKind.Member, model.Symbol);
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

                IEnumerable<INamedTypeSymbol> typeSymbols = DocumentationModel.TypeModels.Select(f => f.TypeSymbol);

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

                return CreateResult(writer, DocumentationKind.ObjectModel);
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

        private DocumentationGeneratorResult CreateResult(DocumentationWriter writer, DocumentationKind kind, ISymbol symbol = null)
        {
            string fileName = UrlProvider.GetFileName(kind);

            return new DocumentationGeneratorResult(writer?.ToString(), GetPath(), kind);

            string GetPath()
            {
                switch (kind)
                {
                    case DocumentationKind.Root:
                    case DocumentationKind.ObjectModel:
                    case DocumentationKind.ExtendedExternalTypes:
                        return fileName;
                    case DocumentationKind.Namespace:
                    case DocumentationKind.Type:
                    case DocumentationKind.Member:
                        return DocumentationUrlProvider.GetUrl(fileName, UrlProvider.GetFolders(symbol), Path.DirectorySeparatorChar);
                    default:
                        throw new ArgumentException("", nameof(kind));
                }
            }
        }
    }
}
