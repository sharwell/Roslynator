// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    //TODO: OmitIEnumerable, InheritedInterfaceMembers, EmphasizeNonInheritedMember
    public class DocumentationOptions
    {
        public DocumentationOptions(
            int maxDerivedItems = 10,
            bool formatBaseList = false,
            bool formatConstraints = false,
            bool indicateInheritedMember = true,
            bool indicateOverridenMember = false,
            bool indicateInterfaceImplementation = false,
            SymbolDisplayFormatProvider formatProvider = null,
            DocumentationParts parts = DocumentationParts.Namespace | DocumentationParts.Type | DocumentationParts.Member,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.AllExceptNestedTypes,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All)
        {
            MaxDerivedItems = maxDerivedItems;
            FormatBaseList = formatBaseList;
            FormatConstraints = formatConstraints;
            IndicateInheritedMember = indicateInheritedMember;
            IndicateOverriddenMember = indicateOverridenMember;
            IndicateInterfaceImplementation = indicateInterfaceImplementation;
            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;
            Parts = parts;
            RootParts = rootParts;
            NamespaceParts = namespaceParts;
            TypeParts = typeParts;
            MemberParts = memberParts;
        }

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public int MaxDerivedItems { get; }

        public SymbolDisplayFormatProvider FormatProvider { get; }

        public bool FormatBaseList { get; }

        public bool FormatConstraints { get; }

        public bool IndicateInheritedMember { get; }

        public bool IndicateOverriddenMember { get; }

        public bool IndicateInterfaceImplementation { get; }

        public DocumentationParts Parts { get; }

        public RootDocumentationParts RootParts { get; }

        public NamespaceDocumentationParts NamespaceParts { get; }

        public TypeDocumentationParts TypeParts { get; }

        public MemberDocumentationParts MemberParts { get; }

        public bool IsPartEnabled(DocumentationParts parts)
        {
            return (Parts & parts) != 0;
        }

        public bool IsPartEnabled(RootDocumentationParts parts)
        {
            return (RootParts & parts) != 0;
        }

        public bool IsPartEnabled(NamespaceDocumentationParts parts)
        {
            return (NamespaceParts & parts) != 0;
        }

        public bool IsPartEnabled(TypeDocumentationParts parts)
        {
            return (TypeParts & parts) != 0;
        }

        public bool IsPartEnabled(MemberDocumentationParts parts)
        {
            return (MemberParts & parts) != 0;
        }
    }
}
