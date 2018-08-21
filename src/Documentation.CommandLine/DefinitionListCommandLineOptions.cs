// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using CommandLine;
using static Roslynator.Documentation.DefinitionListOptions;

namespace Roslynator.Documentation
{
    [Verb("definition-list")]
    public class DefinitionListCommandLineOptions
    {
        [Option(longName: "assembly-references", shortName: 'r', Required = true)]
        public string AssemblyReferences { get; set; }

        [Option(longName: "assemblies", shortName: 'a', Separator = ';', Required = true)]
        public IEnumerable<string> Assemblies { get; set; }

        [Option(longName: "output-path", shortName: 'o', Required = true)]
        public string OutputPath { get; set; }

        [Option(longName: "additional-xml-documentation")]
        public IEnumerable<string> AdditionalXmlDocumentations { get; set; }

        [Option(longName: "indent", Default = DefaultValues.Indent)]
        public bool Indent { get; set; }

        [Option(longName: "indent-chars", Default = DefaultValues.IndentChars)]
        public string IndentChars { get; set; }

        [Option(longName: "open-brace-on-new-line", Default = DefaultValues.OpenBraceOnNewLine)]
        public bool OpenBraceOnNewLine { get; set; }

        [Option(longName: "empty-line-between-members", Default = DefaultValues.EmptyLineBetweenMembers)]
        public bool EmptyLineBetweenMembers { get; set; }

        [Option(longName: "new-line-on-attributes", Default = DefaultValues.NewLineOnAttributes)]
        public bool NewLineOnAttributes { get; set; }

        [Option(longName: "attribute-arguments", Default = DefaultValues.AttributeArguments)]
        public bool AttributeArguments { get; set; }

        [Option(longName: "omit-ienumerable", Default = DefaultValues.OmitIEnumerable)]
        public bool OmitIEnumerable { get; set; }

        [Option(longName: "use-default-literal", Default = DefaultValues.UseDefaultLiteral)]
        public bool UseDefaultLiteral { get; set; }
    }
}
