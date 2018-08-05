// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    //TODO: AddDocumentationComments
    public class DefinitionListOptions
    {
        public DefinitionListOptions(
            bool indent = true,
            string indentChars = "    ",
            bool openBraceOnNewLine = true,
            bool emptyLineBetweenMembers = false,
            bool newLineOnAttributes = true,
            bool attributeArguments = true,
            bool omitIEnumerable = true,
            bool useDefaultLiteral = true)
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

        public bool OpenBraceOnNewLine { get; }

        public bool EmptyLineBetweenMembers { get; }

        public bool NewLineOnAttributes { get; }

        public bool AttributeArguments { get; }

        public bool OmitIEnumerable { get; }

        public bool UseDefaultLiteral { get; }
    }
}
