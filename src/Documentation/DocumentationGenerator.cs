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
        private ImmutableArray<RootDocumentationParts> _enabledAndSortedRootParts;
        private ImmutableArray<TypeDocumentationParts> _enabledAndSortedTypeParts;
        private ImmutableArray<NamespaceDocumentationParts> _enabledAndSortedNamespaceParts;

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

        public DocumentationResources Resources { get; }

        public DocumentationUrlProvider UrlProvider { get; }

        public virtual IComparer<RootDocumentationParts> RootPartComparer
        {
            get { return RootDocumentationPartComparer.Instance; }
        }

        public virtual IComparer<NamespaceDocumentationParts> NamespacePartComparer
        {
            get { return NamespaceDocumentationPartComparer.Instance; }
        }

        public virtual IComparer<TypeDocumentationParts> TypePartComparer
        {
            get { return TypeDocumentationPartComparer.Instance; }
        }

        public virtual IComparer<MemberDocumentationParts> MemberPartComparer
        {
            get { return MemberDocumentationPartComparer.Instance; }
        }

        internal ImmutableArray<RootDocumentationParts> EnabledAndSortedRootParts
        {
            get
            {
                if (_enabledAndSortedRootParts.IsDefault)
                {
                    _enabledAndSortedRootParts = Enum.GetValues(typeof(RootDocumentationParts))
                        .Cast<RootDocumentationParts>()
                        .Where(f => f != RootDocumentationParts.None
                            && f != RootDocumentationParts.All
                            && f != RootDocumentationParts.Types
                            && Options.IsPartEnabled(f))
                        .OrderBy(f => f, RootPartComparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedRootParts;
            }
        }

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
                            && f != TypeDocumentationParts.NestedTypes
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
            DocumentationWriter writer = CreateWriterCore();

            writer.CurrentSymbol = currentSymbol;
            writer.CanCreateMemberLocalUrl = Options.IsPartEnabled(DocumentationParts.Member);
            writer.CanCreateTypeLocalUrl = Options.IsPartEnabled(DocumentationParts.Type);

            return writer;
        }

        protected abstract DocumentationWriter CreateWriterCore();

        public IEnumerable<DocumentationGeneratorResult> Generate(
            string heading = null,
            string extendedExternalTypesHeading = null)
        {
            DocumentationParts parts = Options.DocumentationParts;

            DocumentationGeneratorResult objectModel = default;
            DocumentationGeneratorResult extendedExternalTypes = default;

            if ((parts & DocumentationParts.ExtendedExternalTypes) != 0)
            {
                extendedExternalTypes = GenerateExtendedExternalTypes(extendedExternalTypesHeading ?? Resources.ExtendedExternalTypesTitle);

                if (!extendedExternalTypes.HasContent)
                    parts &= ~DocumentationParts.ExtendedExternalTypes;
            }

            using (DocumentationWriter writer = CreateWriter())
            {
                yield return GenerateRoot(writer, heading, addExtendedExternalTypesLink: (parts & DocumentationParts.ExtendedExternalTypes) != 0);
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

        internal DocumentationGeneratorResult GenerateRoot(
            string heading,
            bool addExtendedExternalTypesLink = false)
        {
            using (DocumentationWriter writer = CreateWriter())
            {
                return GenerateRoot(writer, heading, addExtendedExternalTypesLink: addExtendedExternalTypesLink);
            }
        }

        internal DocumentationGeneratorResult GenerateRoot(
            DocumentationWriter writer,
            string heading,
            bool addExtendedExternalTypesLink = false)
        {
            writer.WriteStartDocument();

            writer.WriteHeading1(heading);

            GenerateRoot(writer, addExtendedExternalTypesLink: addExtendedExternalTypesLink);

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

        private void GenerateNamespaceContent(
            DocumentationWriter writer,
            NamespaceDocumentationModel namespaceModel,
            int headingLevelBase = 0,
            bool addLink = true,
            NamespaceDocumentationParts disabledParts = NamespaceDocumentationParts.None)
        {
            writer.WriteHeading(1 + headingLevelBase, namespaceModel.Symbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces, addLink: addLink);

            foreach (NamespaceDocumentationParts part in EnabledAndSortedNamespaceParts)
            {
                if ((disabledParts & part) != 0)
                    continue;

                switch (part)
                {
                    case NamespaceDocumentationParts.Content:
                        {
                            writer.WriteContent(GetAvailableParts().OrderBy(f => f, NamespacePartComparer).Select(f => Resources.GetHeading(f)));
                            break;
                        }
                    case NamespaceDocumentationParts.Summary:
                        {
                            DocumentationModel.GetXmlDocumentation(namespaceModel.Symbol, Options.PreferredCultureName)?.WriteContentTo(writer, WellKnownTags.Summary);
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
                    headingLevel: 2 + headingLevelBase,
                    Resources.GetName(typeKind),
                    Resources.SummaryTitle,
                    SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters,
                    addLink: Options.IsPartEnabled(DocumentationParts.Type));
            }

            IEnumerable<NamespaceDocumentationParts> GetAvailableParts()
            {
                foreach (NamespaceDocumentationParts part in EnabledAndSortedNamespaceParts)
                {
                    if ((disabledParts & part) == 0
                        && IsAvailable(part))
                    {
                        yield return part;
                    }
                }
            }

            bool IsAvailable(NamespaceDocumentationParts part)
            {
                switch (part)
                {
                    case NamespaceDocumentationParts.Content:
                    case NamespaceDocumentationParts.Summary:
                        {
                            return false;
                        }
                    case NamespaceDocumentationParts.Examples:
                        {
                            return DocumentationModel
                                .GetXmlDocumentation(namespaceModel.Symbol, Options.PreferredCultureName)?
                                .HasElement(WellKnownTags.Example) == true;
                        }
                    case NamespaceDocumentationParts.Remarks:
                        {
                            return DocumentationModel
                                .GetXmlDocumentation(namespaceModel.Symbol, Options.PreferredCultureName)?
                                .HasElement(WellKnownTags.Remarks) == true;
                        }
                    case NamespaceDocumentationParts.Classes:
                        {
                            return namespaceModel.TypeModels.Any(f => f.TypeKind == TypeKind.Class);
                        }
                    case NamespaceDocumentationParts.Structs:
                        {
                            return namespaceModel.TypeModels.Any(f => f.TypeKind == TypeKind.Struct);
                        }
                    case NamespaceDocumentationParts.Interfaces:
                        {
                            return namespaceModel.TypeModels.Any(f => f.TypeKind == TypeKind.Interface);
                        }
                    case NamespaceDocumentationParts.Enums:
                        {
                            return namespaceModel.TypeModels.Any(f => f.TypeKind == TypeKind.Enum);
                        }
                    case NamespaceDocumentationParts.Delegates:
                        {
                            return namespaceModel.TypeModels.Any(f => f.TypeKind == TypeKind.Delegate);
                        }
                    case NamespaceDocumentationParts.SeeAlso:
                        {
                            return DocumentationModel
                                .GetXmlDocumentation(namespaceModel.Symbol, Options.PreferredCultureName)?
                                .SeeAlsoCommentIds()
                                .Any() == true;
                        }
                    default:
                        {
                            throw new InvalidOperationException();
                        }
                }
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

                foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> typesByNamespaces in extendedExternalTypes
                    .OrderBy(f => f.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters))
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
                        writer.WriteList(typesByKind, Resources.GetPluralName(typesByKind.Key), 3, SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters, canCreateExternalUrl: false);
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
                writer.WriteLink(typeSymbol, SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters);
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
                    SymbolDisplayFormats.SimpleDefinition);

                writer.WriteEndDocument();

                return CreateResult(writer, DocumentationKind.Type, typeSymbol);
            }
        }

        private DocumentationGeneratorResult GenerateType(TypeDocumentationModel typeModel)
        {
            INamedTypeSymbol typeSymbol = typeModel.TypeSymbol;

            ImmutableArray<INamedTypeSymbol> nestedTypes = default;

            bool includeInherited = typeSymbol.TypeKind != TypeKind.Interface || Options.InheritedInterfaceMembers;

            using (DocumentationWriter writer = CreateWriter(typeSymbol))
            {
                writer.WriteStartDocument();

                writer.WriteTitle(typeSymbol);

                foreach (TypeDocumentationParts part in EnabledAndSortedTypeParts)
                {
                    switch (part)
                    {
                        case TypeDocumentationParts.Content:
                            {
                                writer.WriteContent(GetAvailableParts().OrderBy(f => f, TypePartComparer).Select(f => Resources.GetHeading(f)));
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
                                writer.WriteProperties(typeModel.GetProperties(includeInherited: includeInherited), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Methods:
                            {
                                writer.WriteMethods(typeModel.GetMethods(includeInherited: includeInherited), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Operators:
                            {
                                writer.WriteOperators(typeModel.GetOperators(includeInherited: true), containingType: typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Events:
                            {
                                writer.WriteEvents(typeModel.GetEvents(includeInherited: includeInherited), containingType: typeSymbol);
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

            IEnumerable<TypeDocumentationParts> GetAvailableParts()
            {
                foreach (TypeDocumentationParts part in EnabledAndSortedTypeParts)
                {
                    if (IsAvailable(part))
                        yield return part;
                }
            }

            bool IsAvailable(TypeDocumentationParts part)
            {
                switch (part)
                {
                    case TypeDocumentationParts.Content:
                    case TypeDocumentationParts.Namespace:
                    case TypeDocumentationParts.Assembly:
                    case TypeDocumentationParts.Obsolete:
                    case TypeDocumentationParts.Summary:
                    case TypeDocumentationParts.Definition:
                    case TypeDocumentationParts.TypeParameters:
                    case TypeDocumentationParts.Parameters:
                    case TypeDocumentationParts.ReturnValue:
                    case TypeDocumentationParts.Inheritance:
                    case TypeDocumentationParts.Attributes:
                    case TypeDocumentationParts.Derived:
                    case TypeDocumentationParts.Implements:
                        {
                            return false;
                        }
                    case TypeDocumentationParts.Examples:
                        {
                            return DocumentationModel
                                .GetXmlDocumentation(typeModel.Symbol, Options.PreferredCultureName)?
                                .HasElement(WellKnownTags.Example) == true;
                        }
                    case TypeDocumentationParts.Remarks:
                        {
                            return DocumentationModel
                                .GetXmlDocumentation(typeModel.Symbol, Options.PreferredCultureName)?
                                .HasElement(WellKnownTags.Remarks) == true;
                        }
                    case TypeDocumentationParts.Constructors:
                        {
                            return typeModel.GetConstructors().Any();
                        }
                    case TypeDocumentationParts.Fields:
                        {
                            if (typeModel.TypeKind == TypeKind.Enum)
                            {
                                return typeModel.GetFields().Any();
                            }
                            else
                            {
                                return typeModel.GetFields(includeInherited: true).Any();
                            }
                        }
                    case TypeDocumentationParts.Properties:
                        {
                            return typeModel.GetProperties(includeInherited: includeInherited).Any();
                        }
                    case TypeDocumentationParts.Methods:
                        {
                            return typeModel.GetMethods(includeInherited: includeInherited).Any();
                        }
                    case TypeDocumentationParts.Operators:
                        {
                            return typeModel.GetOperators(includeInherited: true).Any();
                        }
                    case TypeDocumentationParts.Events:
                        {
                            return typeModel.GetEvents(includeInherited: includeInherited).Any();
                        }
                    case TypeDocumentationParts.ExplicitInterfaceImplementations:
                        {
                            return typeModel.GetExplicitInterfaceImplementations().Any();
                        }
                    case TypeDocumentationParts.ExtensionMethods:
                        {
                            return typeModel.GetExtensionMethods().Any();
                        }
                    case TypeDocumentationParts.Classes:
                        {
                            if (nestedTypes.IsDefault)
                                nestedTypes = typeSymbol.GetTypeMembers();

                            return nestedTypes.Any(f => f.TypeKind == TypeKind.Class && DocumentationModel.IsVisible(f));
                        }
                    case TypeDocumentationParts.Structs:
                        {
                            if (nestedTypes.IsDefault)
                                nestedTypes = typeSymbol.GetTypeMembers();

                            return nestedTypes.Any(f => f.TypeKind == TypeKind.Struct && DocumentationModel.IsVisible(f));
                        }
                    case TypeDocumentationParts.Interfaces:
                        {
                            if (nestedTypes.IsDefault)
                                nestedTypes = typeSymbol.GetTypeMembers();

                            return nestedTypes.Any(f => f.TypeKind == TypeKind.Interface && DocumentationModel.IsVisible(f));
                        }
                    case TypeDocumentationParts.Enums:
                        {
                            if (nestedTypes.IsDefault)
                                nestedTypes = typeSymbol.GetTypeMembers();

                            return nestedTypes.Any(f => f.TypeKind == TypeKind.Enum && DocumentationModel.IsVisible(f));
                        }
                    case TypeDocumentationParts.Delegates:
                        {
                            if (nestedTypes.IsDefault)
                                nestedTypes = typeSymbol.GetTypeMembers();

                            return nestedTypes.Any(f => f.TypeKind == TypeKind.Delegate && DocumentationModel.IsVisible(f));
                        }
                    case TypeDocumentationParts.SeeAlso:
                        {
                            return DocumentationModel
                                .GetXmlDocumentation(typeModel.Symbol, Options.PreferredCultureName)?
                                .SeeAlsoCommentIds()
                                .Any() == true;
                        }
                    default:
                        {
                            throw new InvalidOperationException();
                        }
                }
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

        private void GenerateRoot(DocumentationWriter writer, bool addExtendedExternalTypesLink = false)
        {
            SymbolDisplayFormat format = SymbolDisplayFormats.TypeNameAndContainingTypesAndTypeParameters;

            IEnumerable<INamedTypeSymbol> typeSymbols = DocumentationModel.TypeModels.Select(f => f.TypeSymbol);

            foreach (RootDocumentationParts part in EnabledAndSortedRootParts)
            {
                switch (part)
                {
                    case RootDocumentationParts.Content:
                        {
                            writer.WriteContent(GetAvailableParts().OrderBy(f => f, RootPartComparer).Select(f => Resources.GetHeading(f)));
                            break;
                        }

                    case RootDocumentationParts.ExtendedExternalTypesLink:
                        {
                            if (addExtendedExternalTypesLink)
                            {
                                writer.WriteStartBulletItem();
                                writer.WriteLink(Resources.ExtendedExternalTypesTitle, WellKnownNames.ExtendedExternalTypesFileName);
                                writer.WriteEndBulletItem();
                            }

                            break;
                        }
                    case RootDocumentationParts.Namespaces:
                        {
                            writer.WriteList(DocumentationModel.NamespaceModels.Select(f => f.Symbol), Resources.NamespacesTitle, 2, SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces);
                            break;
                        }
                    case RootDocumentationParts.Classes:
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

                                writer.WriteHeading2(Resources.GetPluralName(TypeKind.Class));
                                WriteBulletItem(objectType, nodes);
                            }

                            break;
                        }
                    case RootDocumentationParts.StaticClasses:
                        {
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
                    case RootDocumentationParts.Structs:
                        {
                            WriteTypes(TypeKind.Struct);
                            break;
                        }
                    case RootDocumentationParts.Interfaces:
                        {
                            WriteTypes(TypeKind.Interface);
                            break;
                        }
                    case RootDocumentationParts.Enums:
                        {
                            WriteTypes(TypeKind.Enum);
                            break;
                        }
                    case RootDocumentationParts.Delegates:
                        {
                            WriteTypes(TypeKind.Delegate);
                            break;
                        }
                }
            }

            void WriteTypes(TypeKind typeKind)
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
            }

            void WriteBulletItem(ITypeSymbol baseType, HashSet<ITypeSymbol> nodes)
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(baseType, format);

                nodes.Remove(baseType);

                foreach (ITypeSymbol derivedType in nodes
                    .Where(f => f.BaseType?.OriginalDefinition == baseType.OriginalDefinition)
                    .OrderBy(f => f.ToDisplayString(format))
                    .ToList())
                {
                    WriteBulletItem(derivedType, nodes);
                }

                writer.WriteEndBulletItem();
            }

            IEnumerable<RootDocumentationParts> GetAvailableParts()
            {
                foreach (RootDocumentationParts part in EnabledAndSortedRootParts)
                {
                    if (IsAvailable(part))
                        yield return part;
                }
            }

            bool IsAvailable(RootDocumentationParts part)
            {
                switch (part)
                {
                    case RootDocumentationParts.Content:
                    case RootDocumentationParts.ExtendedExternalTypesLink:
                        {
                            return false;
                        }
                    case RootDocumentationParts.Namespaces:
                        {
                            return typeSymbols.Any();
                        }
                    case RootDocumentationParts.Classes:
                        {
                            return typeSymbols.Any(f => !f.IsStatic && f.TypeKind == TypeKind.Class);
                        }
                    case RootDocumentationParts.StaticClasses:
                        {
                            return typeSymbols.Any(f => f.IsStatic && f.TypeKind == TypeKind.Class);
                        }
                    case RootDocumentationParts.Structs:
                        {
                            return typeSymbols.Any(f => f.TypeKind == TypeKind.Struct);
                        }
                    case RootDocumentationParts.Interfaces:
                        {
                            return typeSymbols.Any(f => f.TypeKind == TypeKind.Interface);
                        }
                    case RootDocumentationParts.Enums:
                        {
                            return typeSymbols.Any(f => f.TypeKind == TypeKind.Enum);
                        }
                    case RootDocumentationParts.Delegates:
                        {
                            return typeSymbols.Any(f => f.TypeKind == TypeKind.Delegate);
                        }
                    default:
                        {
                            throw new InvalidOperationException();
                        }
                }
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
