// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    //TODO: SystemNamespaceFirst
    public class DocumentationOptions
    {
        internal static class DefaultValues
        {
            public const int MaxDerivedItems = 10;
            public const bool FormatDefinitionBaseList = false;
            public const bool FormatDefinitionConstraints = false;
            public const bool IndicateObsolete = true;
            public const bool IndicateInheritedMember = true;
            public const bool IndicateOverriddenMember = false;
            public const bool IndicateInterfaceImplementation = false;
            public const bool AttributeArguments = true;
            public const bool InheritedInterfaceMembers = true;
            public const bool OmitIEnumerable = true;
        }

        public DocumentationOptions(
            string preferredCultureName = null,
            string baseLocalUrl = null,
            int maxDerivedItems = DefaultValues.MaxDerivedItems,
            bool formatDefinitionBaseList = DefaultValues.FormatDefinitionBaseList,
            bool formatDefinitionConstraints = DefaultValues.FormatDefinitionConstraints,
            bool indicateObsolete = DefaultValues.IndicateObsolete,
            bool indicateInheritedMember = DefaultValues.IndicateInheritedMember,
            bool indicateOverriddenMember = DefaultValues.IndicateOverriddenMember,
            bool indicateInterfaceImplementation = DefaultValues.IndicateInterfaceImplementation,
            bool attributeArguments = DefaultValues.AttributeArguments,
            bool inheritedInterfaceMembers = DefaultValues.InheritedInterfaceMembers,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            DocumentationParts documentationParts = DocumentationParts.All,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.All,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All)
        {
            PreferredCultureName = preferredCultureName;
            BaseLocalUrl = baseLocalUrl;
            MaxDerivedItems = maxDerivedItems;
            FormatDefinitionBaseList = formatDefinitionBaseList;
            FormatDefinitionConstraints = formatDefinitionConstraints;
            IndicateObsolete = indicateObsolete;
            IndicateInheritedMember = indicateInheritedMember;
            IndicateOverriddenMember = indicateOverriddenMember;
            IndicateInterfaceImplementation = indicateInterfaceImplementation;
            AttributeArguments = attributeArguments;
            InheritedInterfaceMembers = inheritedInterfaceMembers;
            OmitIEnumerable = omitIEnumerable;
            DocumentationParts = documentationParts;
            RootParts = rootParts;
            NamespaceParts = namespaceParts;
            TypeParts = typeParts;
            MemberParts = memberParts;
        }

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public string PreferredCultureName { get; }

        public string BaseLocalUrl { get; }

        public int MaxDerivedItems { get; }

        public bool FormatDefinitionBaseList { get; }

        public bool FormatDefinitionConstraints { get; }

        public bool IndicateObsolete { get; }

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
