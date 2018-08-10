// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using CommandLine;

namespace Roslynator.Documentation
{
    public class CommandLineOptions
    {
        [Option(longName: "assemblyReferences", Required = true)]
        public string AssemblyReferences { get; set; }

        [Option(longName: "assemblies", Separator = ';', Required = true)]
        public IEnumerable<string> Assemblies { get; set; }

        [Option(longName: "outputDirectory", Required = true)]
        public string OutputDirectory { get; set; }

        [Option(longName: "heading", Required = true)]
        public string Heading { get; set; }

        [Option(longName: "environment", Required = true)]
        public string Environment { get; set; }

        [Option(longName: "documentationParts")]
        public IEnumerable<string> DocumentationParts { get; set; }

        [Option(longName: "namespaceParts")]
        public IEnumerable<string> NamespaceParts { get; set; }

        [Option(longName: "typeParts")]
        public IEnumerable<string> TypeParts { get; set; }

        [Option(longName: "memberParts")]
        public IEnumerable<string> MemberParts { get; set; }

        [Option(longName: "preferredCultureName")]
        public string PreferredCultureName { get; set; }

        [Option(longName: "formatDefinitionBaseList")]
        public bool FormatDefinitionBaseList { get; set; }

        [Option(longName: "formatDefinitionConstraints")]
        public bool FormatDefinitionConstraints { get; set; }

        [Option(longName: "maxDerivedItems", Default = -1)]
        public int MaxDerivedItems { get; set; }

        [Option(longName: "indicateObsolete")]
        public bool IndicateObsolete { get; set; }

        [Option(longName: "indicateInheritedMember")]
        public bool IndicateInheritedMember { get; set; }

        [Option(longName: "indicateOverriddenMember")]
        public bool IndicateOverriddenMember { get; set; }

        [Option(longName: "indicateInterfaceImplementation")]
        public bool IndicateInterfaceImplementation { get; set; }

        [Option(longName: "attributeArguments")]
        public bool AttributeArguments { get; set; }

        [Option(longName: "inheritedInterfaceMembers")]
        public bool InheritedInterfaceMembers { get; set; }

        [Option(longName: "omitIEnumerable")]
        public bool OmitIEnumerable { get; set; }
    }
}
