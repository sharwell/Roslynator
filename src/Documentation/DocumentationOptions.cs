// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    //TODO: OmitIEnumerable, InheritedInterfaceMembers, EmphasizeNonInheritedMember
    public class DocumentationOptions
    {
        public DocumentationOptions(
            int maxDerivedItems = 10,
            bool formatBaseList = false,
            bool formatConstraints = false,
            SymbolDisplayFormatProvider formatProvider = null,
            DocumentationParts parts = DocumentationParts.Namespace | DocumentationParts.Type | DocumentationParts.Member,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.All,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All)
        {
            MaxDerivedItems = maxDerivedItems;
            FormatBaseList = formatBaseList;
            FormatConstraints = formatConstraints;
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

        internal bool IsNamespacePartEnabled(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return IsPartEnabled(NamespaceDocumentationParts.Classes);
                case TypeKind.Delegate:
                    return IsPartEnabled(NamespaceDocumentationParts.Delegates);
                case TypeKind.Enum:
                    return IsPartEnabled(NamespaceDocumentationParts.Enums);
                case TypeKind.Interface:
                    return IsPartEnabled(NamespaceDocumentationParts.Interfaces);
                case TypeKind.Struct:
                    return IsPartEnabled(NamespaceDocumentationParts.Structs);
                default:
                    return false;
            }
        }
    }
}
