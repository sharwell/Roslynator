// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DeclarationListOptions
    {
        private readonly ImmutableArray<MetadataName> _ignoredNamespaces;
        private readonly ImmutableArray<MetadataName> _ignoredTypes;

        public DeclarationListOptions(
            IEnumerable<string> ignoredNamespaces = null,
            IEnumerable<string> ignoredTypes = null,
            bool indent = DefaultValues.Indent,
            string indentChars = DefaultValues.IndentChars,
            bool newLineBeforeOpenBrace = DefaultValues.NewLineBeforeOpenBrace,
            bool emptyLineBetweenMembers = DefaultValues.EmptyLineBetweenMembers,
            bool splitAttributes = DefaultValues.SplitAttributes,
            bool includeAttributeArguments = DefaultValues.IncludeAttributeArguments,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            bool useDefaultLiteral = DefaultValues.UseDefaultLiteral)
        {
            _ignoredNamespaces = ignoredNamespaces?.Select(name => MetadataName.ParseNamespaceName(name)).ToImmutableArray() ?? default;
            _ignoredTypes = ignoredTypes?.Select(name => MetadataName.ParseTypeName(name)).ToImmutableArray() ?? default;

            IgnoredNamespaces = ignoredNamespaces?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            IgnoredTypes = ignoredTypes?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
            Indent = indent;
            IndentChars = indentChars;
            NewLineBeforeOpenBrace = newLineBeforeOpenBrace;
            EmptyLineBetweenMembers = emptyLineBetweenMembers;
            SplitAttributes = splitAttributes;
            IncludeAttributeArguments = includeAttributeArguments;
            OmitIEnumerable = omitIEnumerable;
            UseDefaultLiteral = useDefaultLiteral;
        }

        public static DeclarationListOptions Default { get; } = new DeclarationListOptions();

        public ImmutableArray<string> IgnoredNamespaces { get; }

        public ImmutableArray<string> IgnoredTypes { get; }

        public bool Indent { get; }

        public string IndentChars { get; }

        public bool NewLineBeforeOpenBrace { get; }

        public bool EmptyLineBetweenMembers { get; }

        public bool SplitAttributes { get; }

        public bool IncludeAttributeArguments { get; }

        public bool OmitIEnumerable { get; }

        public bool UseDefaultLiteral { get; }

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

        internal static class DefaultValues
        {
            public const bool Indent = true;
            public const string IndentChars = "    ";
            public const bool NewLineBeforeOpenBrace = true;
            public const bool EmptyLineBetweenMembers = false;
            public const bool SplitAttributes = true;
            public const bool IncludeAttributeArguments = true;
            public const bool OmitIEnumerable = true;
            public const bool UseDefaultLiteral = true;
        }
    }
}
