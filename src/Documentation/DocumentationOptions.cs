// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationOptions
    {
        private readonly ImmutableArray<MetadataName> _ignoredNamespaces;
        private readonly ImmutableArray<MetadataName> _ignoredTypes;

        public DocumentationOptions(
            IEnumerable<string> ignoredNamespaces = null,
            IEnumerable<string> ignoredTypes = null,
            string preferredCultureName = null,
            string baseLocalUrl = null,
            int maxDerivedTypes = DefaultValues.MaxDerivedTypes,
            bool includeClassHierarchy = DefaultValues.IncludeClassHierarchy,
            bool includeContainingNamespace = DefaultValues.IncludeContainingNamespace,
            bool placeSystemNamespaceFirst = DefaultValues.PlaceSystemNamespaceFirst,
            bool formatDeclarationBaseList = DefaultValues.FormatDeclarationBaseList,
            bool formatDeclarationConstraints = DefaultValues.FormatDeclarationConstraints,
            bool markObsolete = DefaultValues.MarkObsolete,
            bool includeMemberInheritedFrom = DefaultValues.IncludeMemberInheritedFrom,
            bool includeMemberOverrides = DefaultValues.IncludeMemberOverrides,
            bool includeMemberImplements = DefaultValues.IncludeMemberImplements,
            bool includeMemberConstantValue = DefaultValues.IncludeMemberConstantValue,
            bool includeInheritedInterfaceMembers = DefaultValues.IncludeInheritedInterfaceMembers,
            bool includeAttributeArguments = DefaultValues.IncludeAttributeArguments,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            DocumentationDepth depth = DefaultValues.Depth,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.All,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All)
        {
            if (maxDerivedTypes < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDerivedTypes), maxDerivedTypes, "Maximum number of derived items must be greater than or equal to 0.");

            _ignoredNamespaces = ignoredNamespaces?.Select(name => MetadataName.ParseNamespaceName(name)).ToImmutableArray() ?? default;
            _ignoredTypes = ignoredTypes?.Select(name => MetadataName.ParseTypeName(name)).ToImmutableArray() ?? default;

            IgnoredNamespaces = ignoredNamespaces?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            IgnoredTypes = ignoredTypes?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            PreferredCultureName = preferredCultureName;
            BaseLocalUrl = baseLocalUrl;
            MaxDerivedTypes = maxDerivedTypes;
            IncludeClassHierarchy = includeClassHierarchy;
            IncludeContainingNamespace = includeContainingNamespace;
            PlaceSystemNamespaceFirst = placeSystemNamespaceFirst;
            FormatDeclarationBaseList = formatDeclarationBaseList;
            FormatDeclarationConstraints = formatDeclarationConstraints;
            MarkObsolete = markObsolete;
            IncludeMemberInheritedFrom = includeMemberInheritedFrom;
            IncludeMemberOverrides = includeMemberOverrides;
            IncludeMemberImplements = includeMemberImplements;
            IncludeMemberConstantValue = includeMemberConstantValue;
            IncludeInheritedInterfaceMembers = includeInheritedInterfaceMembers;
            IncludeAttributeArguments = includeAttributeArguments;
            OmitIEnumerable = omitIEnumerable;
            Depth = depth;
            RootParts = rootParts;
            NamespaceParts = namespaceParts;
            TypeParts = typeParts;
            MemberParts = memberParts;
        }

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public ImmutableArray<string> IgnoredNamespaces { get; }

        public ImmutableArray<string> IgnoredTypes { get; }

        public string PreferredCultureName { get; }

        public string BaseLocalUrl { get; }

        public int MaxDerivedTypes { get; }

        public bool IncludeClassHierarchy { get; }

        public bool IncludeContainingNamespace { get; }

        public bool PlaceSystemNamespaceFirst { get; }

        public bool FormatDeclarationBaseList { get; }

        public bool FormatDeclarationConstraints { get; }

        public bool MarkObsolete { get; }

        public bool IncludeMemberInheritedFrom { get; }

        public bool IncludeMemberOverrides { get; }

        public bool IncludeMemberImplements { get; }

        public bool IncludeMemberConstantValue { get; }

        public bool IncludeInheritedInterfaceMembers { get; }

        public bool IncludeAttributeArguments { get; }

        public bool OmitIEnumerable { get; }

        public DocumentationDepth Depth { get; }

        public RootDocumentationParts RootParts { get; }

        public NamespaceDocumentationParts NamespaceParts { get; }

        public TypeDocumentationParts TypeParts { get; }

        public MemberDocumentationParts MemberParts { get; }

        internal bool ShouldBeIgnored(INamespaceSymbol namespaceSymbol)
        {
            if (!_ignoredNamespaces.IsDefault)
            {
                foreach (MetadataName name in _ignoredNamespaces)
                {
                    if (namespaceSymbol.HasMetadataName(name))
                        return true;
                }
            }

            return false;
        }

        internal bool ShouldBeIgnored(INamedTypeSymbol typeSymbol)
        {
            if (ShouldBeIgnored(typeSymbol.ContainingNamespace))
                return true;

            if (!_ignoredTypes.IsDefault)
            {
                foreach (MetadataName name in _ignoredTypes)
                {
                    if (typeSymbol.HasMetadataName(name))
                        return true;
                }
            }

            return false;
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

        internal static class DefaultValues
        {
            public const int MaxDerivedTypes = 5;
            public const bool IncludeClassHierarchy = true;
            public const bool IncludeContainingNamespace = true;
            public const bool PlaceSystemNamespaceFirst = true;
            public const bool FormatDeclarationBaseList = true;
            public const bool FormatDeclarationConstraints = true;
            public const bool MarkObsolete = true;
            public const bool IncludeMemberInheritedFrom = true;
            public const bool IncludeMemberOverrides = true;
            public const bool IncludeMemberImplements = true;
            public const bool IncludeMemberConstantValue = true;
            public const bool IncludeInheritedInterfaceMembers = false;
            public const bool IncludeAttributeArguments = true;
            public const bool OmitIEnumerable = true;
            public const DocumentationDepth Depth = DocumentationDepth.Member;
        }
    }
}
