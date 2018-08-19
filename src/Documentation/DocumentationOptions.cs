// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    public class DocumentationOptions
    {
        public DocumentationOptions(
            string preferredCultureName = null,
            string baseLocalUrl = null,
            int maxDerivedItems = DefaultValues.MaxDerivedItems,
            bool classHierarchy = DefaultValues.ClassHierarchy,
            bool addContainingNamespace = DefaultValues.AddContainingNamespace,
            bool systemNamespaceFirst = DefaultValues.SystemNamespaceFirst,
            bool formatDefinitionBaseList = DefaultValues.FormatDefinitionBaseList,
            bool formatDefinitionConstraints = DefaultValues.FormatDefinitionConstraints,
            bool indicateObsolete = DefaultValues.IndicateObsolete,
            bool indicateInheritedMember = DefaultValues.IndicateInheritedMember,
            bool indicateOverriddenMember = DefaultValues.IndicateOverriddenMember,
            bool indicateInterfaceImplementation = DefaultValues.IndicateInterfaceImplementation,
            bool addConstantValue = DefaultValues.AddConstantValue,
            bool attributeArguments = DefaultValues.AttributeArguments,
            bool inheritedInterfaceMembers = DefaultValues.InheritedInterfaceMembers,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            DocumentationDepth depth = DefaultValues.Depth,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.All,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All)
        {
            if (maxDerivedItems < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDerivedItems), maxDerivedItems, "Maximum number of derived items must be greater than or equal to 0.");

            PreferredCultureName = preferredCultureName;
            BaseLocalUrl = baseLocalUrl;
            MaxDerivedItems = maxDerivedItems;
            ClassHierarchy = classHierarchy;
            AddContainingNamespace = addContainingNamespace;
            SystemNamespaceFirst = systemNamespaceFirst;
            FormatDefinitionBaseList = formatDefinitionBaseList;
            FormatDefinitionConstraints = formatDefinitionConstraints;
            IndicateObsolete = indicateObsolete;
            IndicateInheritedMember = indicateInheritedMember;
            IndicateOverriddenMember = indicateOverriddenMember;
            IndicateInterfaceImplementation = indicateInterfaceImplementation;
            AddConstantValue = addConstantValue;
            AttributeArguments = attributeArguments;
            InheritedInterfaceMembers = inheritedInterfaceMembers;
            OmitIEnumerable = omitIEnumerable;
            Depth = depth;
            RootParts = rootParts;
            NamespaceParts = namespaceParts;
            TypeParts = typeParts;
            MemberParts = memberParts;
        }

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public string PreferredCultureName { get; }

        public string BaseLocalUrl { get; }

        public int MaxDerivedItems { get; }

        public bool ClassHierarchy { get; }

        public bool AddContainingNamespace { get; }

        public bool SystemNamespaceFirst { get; }

        public bool FormatDefinitionBaseList { get; }

        public bool FormatDefinitionConstraints { get; }

        public bool IndicateObsolete { get; }

        public bool IndicateInheritedMember { get; }

        public bool IndicateOverriddenMember { get; }

        public bool IndicateInterfaceImplementation { get; }

        public bool AddConstantValue { get; }

        public bool AttributeArguments { get; }

        public bool InheritedInterfaceMembers { get; }

        public bool OmitIEnumerable { get; }

        public DocumentationDepth Depth { get; }

        public RootDocumentationParts RootParts { get; }

        public NamespaceDocumentationParts NamespaceParts { get; }

        public TypeDocumentationParts TypeParts { get; }

        public MemberDocumentationParts MemberParts { get; }

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

        internal static class DefaultValues
        {
            public const int MaxDerivedItems = 10;
            public const bool ClassHierarchy = true;
            public const bool AddContainingNamespace = true;
            public const bool SystemNamespaceFirst = true;
            public const bool FormatDefinitionBaseList = true;
            public const bool FormatDefinitionConstraints = true;
            public const bool IndicateObsolete = true;
            public const bool IndicateInheritedMember = true;
            public const bool IndicateOverriddenMember = true;
            public const bool IndicateInterfaceImplementation = true;
            public const bool AddConstantValue = true;
            public const bool AttributeArguments = true;
            public const bool InheritedInterfaceMembers = true; //TODO: InheritedInterfaceMembers default value
            public const bool OmitIEnumerable = true;
            public const DocumentationDepth Depth =  DocumentationDepth.Member;
        }
    }
}
