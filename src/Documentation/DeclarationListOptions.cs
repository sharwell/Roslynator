// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DeclarationListOptions
    {
        private readonly ImmutableArray<MetadataName> _ignoredMetadataNames;

        public DeclarationListOptions(
            IEnumerable<string> ignoredNames = null,
            bool indent = DefaultValues.Indent,
            string indentChars = DefaultValues.IndentChars,
            bool newLineBeforeOpenBrace = DefaultValues.NewLineBeforeOpenBrace,
            bool emptyLineBetweenMembers = DefaultValues.EmptyLineBetweenMembers,
            bool splitAttributes = DefaultValues.SplitAttributes,
            bool includeAttributeArguments = DefaultValues.IncludeAttributeArguments,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            bool useDefaultLiteral = DefaultValues.UseDefaultLiteral)
        {
            _ignoredMetadataNames = ignoredNames?.Select(name => MetadataName.Parse(name)).ToImmutableArray() ?? default;

            IgnoredNames = ignoredNames?.ToImmutableArray() ?? ImmutableArray<string>.Empty;
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

        public ImmutableArray<string> IgnoredNames { get; }

        public bool Indent { get; }

        public string IndentChars { get; }

        public bool NewLineBeforeOpenBrace { get; }

        public bool EmptyLineBetweenMembers { get; }

        public bool SplitAttributes { get; }

        public bool IncludeAttributeArguments { get; }

        public bool OmitIEnumerable { get; }

        public bool UseDefaultLiteral { get; }

        internal bool ShouldBeIgnored(ISymbol symbol)
        {
            if (!_ignoredMetadataNames.IsDefault)
            {
                foreach (MetadataName name in _ignoredMetadataNames)
                {
                    if (symbol.HasMetadataName(name))
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
