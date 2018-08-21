// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    public class DefinitionListOptions
    {
        public DefinitionListOptions(
            bool indent = DefaultValues.Indent,
            string indentChars = DefaultValues.IndentChars,
            bool openBraceOnNewLine = DefaultValues.OpenBraceOnNewLine,
            bool emptyLineBetweenMembers = DefaultValues.EmptyLineBetweenMembers,
            bool newLineOnAttributes = DefaultValues.NewLineOnAttributes,
            bool attributeArguments = DefaultValues.AttributeArguments,
            bool omitIEnumerable = DefaultValues.OmitIEnumerable,
            bool useDefaultLiteral = DefaultValues.UseDefaultLiteral)
        {
            Indent = indent;
            IndentChars = indentChars;
            OpenBraceOnNewLine = openBraceOnNewLine;
            EmptyLineBetweenMembers = emptyLineBetweenMembers;
            NewLineOnAttributes = newLineOnAttributes;
            AttributeArguments = attributeArguments;
            OmitIEnumerable = omitIEnumerable;
            UseDefaultLiteral = useDefaultLiteral;
        }

        public static DefinitionListOptions Default { get; } = new DefinitionListOptions();

        public bool Indent { get; }

        public string IndentChars { get; }

        //TODO: NewLineBeforeOpenBrace
        public bool OpenBraceOnNewLine { get; }

        public bool EmptyLineBetweenMembers { get; }

        //TODO: EachAttributeOnSeparateLine, NewLineBeforeAttribute
        public bool NewLineOnAttributes { get; }

        //TODO: AddAttributeArguments, OmitAttributeArguments, IncludeAttributeArguments
        public bool AttributeArguments { get; }

        public bool OmitIEnumerable { get; }

        public bool UseDefaultLiteral { get; }

        internal static class DefaultValues
        {
            public const bool Indent = true;
            public const string IndentChars = "    ";
            public const bool OpenBraceOnNewLine = true;
            public const bool EmptyLineBetweenMembers = false;
            public const bool NewLineOnAttributes = true;
            public const bool AttributeArguments = true;
            public const bool OmitIEnumerable = true;
            public const bool UseDefaultLiteral = true;
        }
    }
}
