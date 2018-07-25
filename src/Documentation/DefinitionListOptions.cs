// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    public class DefinitionListOptions
    {
        public DefinitionListOptions(bool useDefaultLiteral = true, bool indent = true, string indentChars = "  ")
        {
            UseDefaultLiteral = useDefaultLiteral;
            Indent = indent;
            IndentChars = indentChars;
        }

        public static DefinitionListOptions Default { get; } = new DefinitionListOptions();

        public bool UseDefaultLiteral { get; }

        public bool Indent { get; }

        public string IndentChars { get; }
    }
}
