// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    public class DocumentationOptions
    {
        public DocumentationOptions(
            string preferredCultureName = null,
            int maxDerivedItems = 10,
            bool formatDefinitionBaseList = false,
            bool formatDefinitionConstraints = false,
            bool indicateInheritedMember = true,
            bool indicateOverriddenMember = false,
            bool indicateInterfaceImplementation = false,
            bool attributeArguments = true,
            bool inheritedInterfaceMembers = true,
            bool omitIEnumerable = true,
            SymbolDisplayFormatProvider formatProvider = null,
            DocumentationParts parts = DocumentationParts.Namespace | DocumentationParts.Type | DocumentationParts.Member,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.AllExceptNestedTypes,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All)
        {
            PreferredCultureName = preferredCultureName;
            MaxDerivedItems = maxDerivedItems;
            FormatDefinitionBaseList = formatDefinitionBaseList;
            FormatDefinitionConstraints = formatDefinitionConstraints;
            IndicateInheritedMember = indicateInheritedMember;
            IndicateOverriddenMember = indicateOverriddenMember;
            IndicateInterfaceImplementation = indicateInterfaceImplementation;
            AttributeArguments = attributeArguments;
            InheritedInterfaceMembers = inheritedInterfaceMembers;
            OmitIEnumerable = omitIEnumerable;
            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;
            DocumentationParts = parts;
            RootParts = rootParts;
            NamespaceParts = namespaceParts;
            TypeParts = typeParts;
            MemberParts = memberParts;
        }

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public string PreferredCultureName { get; }

        public int MaxDerivedItems { get; }

        public SymbolDisplayFormatProvider FormatProvider { get; }

        public bool FormatDefinitionBaseList { get; }

        public bool FormatDefinitionConstraints { get; }

        public bool IndicateInheritedMember { get; }

        public bool IndicateOverriddenMember { get; }

        public bool IndicateInterfaceImplementation { get; }

        public bool AttributeArguments { get; }

        public bool InheritedInterfaceMembers { get; }

        public bool OmitIEnumerable { get; }

        public DocumentationParts DocumentationParts { get; }

        public RootDocumentationParts RootParts { get; }

        public NamespaceDocumentationParts NamespaceParts { get; }

        public TypeDocumentationParts TypeParts { get; }

        public MemberDocumentationParts MemberParts { get; }

        internal bool IsPartEnabled(DocumentationParts parts)
        {
            return (DocumentationParts & parts) != 0;
        }

        internal bool IsPartEnabled(RootDocumentationParts parts)
        {
            return (RootParts & parts) != 0;
        }

        internal bool IsPartEnabled(NamespaceDocumentationParts parts)
        {
            return (NamespaceParts & parts) != 0;
        }

        internal bool IsPartEnabled(TypeDocumentationParts parts)
        {
            return (TypeParts & parts) != 0;
        }

        internal bool IsPartEnabled(MemberDocumentationParts parts)
        {
            return (MemberParts & parts) != 0;
        }
    }
}
