// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    public class DeclarationListOptions
    {
        public DeclarationListOptions(
            bool indent = DefaultValues.Indent,
            string indentChars = DefaultValues.IndentChars,
            bool newLineBeforeOpenBrace = DefaultValues.NewLineBeforeOpenBrace,
            bool emptyLineBetweenMembers = DefaultValues.EmptyLineBetweenMembers,
            bool splitAttributes = DefaultValues.SplitAttributes,
            bool includeAttributeArguments = DefaultValues.IncludeAttributeArguments,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            bool useDefaultLiteral = DefaultValues.UseDefaultLiteral)
        {
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

        public bool Indent { get; }

        public string IndentChars { get; }

        public bool NewLineBeforeOpenBrace { get; }

        public bool EmptyLineBetweenMembers { get; }

        public bool SplitAttributes { get; }

        public bool IncludeAttributeArguments { get; }

        public bool OmitIEnumerable { get; }

        public bool UseDefaultLiteral { get; }

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
