// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using CommandLine;
using static Roslynator.Documentation.DeclarationListOptions;

namespace Roslynator.Documentation
{
    [Verb("declarations")]
    public class DeclarationsCommandLineOptions
    {
        [Option(longName: "assemblies", shortName: 'a', Required = true)]
        public IEnumerable<string> Assemblies { get; set; }

        [Option(longName: "output", shortName: 'o', Required = true)]
        public string OutputPath { get; set; }

        [Option(longName: "references", shortName: 'r', Required = true)]
        public string References { get; set; }

        [Option(longName: "additional-xml-documentation")]
        public IEnumerable<string> AdditionalXmlDocumentation { get; set; }

        [Option(longName: "empty-line-between-members", Default = DefaultValues.EmptyLineBetweenMembers)]
        public bool EmptyLineBetweenMembers { get; set; }

        [Option(longName: "format-base-list", Default = DefaultValues.FormatBaseList)]
        public bool FormatBaseList { get; set; }

        [Option(longName: "format-constraints", Default = DefaultValues.FormatConstraints)]
        public bool FormatConstraints { get; set; }

        [Option(longName: "ignored-names")]
        public IEnumerable<string> IgnoredNames { get; set; }

        [Option(longName: "include-attribute-arguments", Default = DefaultValues.IncludeAttributeArguments)]
        public bool IncludeAttributeArguments { get; set; }

        [Option(longName: "indent", Default = DefaultValues.Indent)]
        public bool Indent { get; set; }

        [Option(longName: "indent-chars", Default = DefaultValues.IndentChars)]
        public string IndentChars { get; set; }

        [Option(longName: "new-line-before-open-brace", Default = DefaultValues.NewLineBeforeOpenBrace)]
        public bool NewLineBeforeOpenBrace { get; set; }

        [Option(longName: "omit-ienumerable", Default = DefaultValues.OmitIEnumerable)]
        public bool OmitIEnumerable { get; set; }

        [Option(longName: "split-attributes", Default = DefaultValues.SplitAttributes)]
        public bool SplitAttributes { get; set; }

        [Option(longName: "use-default-literal", Default = DefaultValues.UseDefaultLiteral)]
        public bool UseDefaultLiteral { get; set; }
    }
}
