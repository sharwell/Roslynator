// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using CommandLine;
using static Roslynator.Documentation.DocumentationOptions;

namespace Roslynator.Documentation
{
    [Verb("doc")]
    public class DocumentationCommandLineOptions
    {
        [Option(longName: "assembly-references", shortName: 'r', Required = true)]
        public string AssemblyReferences { get; set; }

        [Option(longName: "assemblies", shortName: 'a', Separator = ';', Required = true)]
        public IEnumerable<string> Assemblies { get; set; }

        [Option(longName: "output-directory", shortName: 'o', Required = true)]
        public string OutputDirectory { get; set; }

        [Option(longName: "heading", shortName: 'h', Required = true)]
        public string Heading { get; set; }

        [Option(longName: "mode", shortName: 'm', Required = true)]
        public string Mode { get; set; }

        [Option(longName: "additional-xml-documentation")]
        public IEnumerable<string> AdditionalXmlDocumentations { get; set; }

        [Option(longName: "depth", Default = DefaultValues.Depth)]
        public DocumentationDepth Depth { get; set; }

        [Option(longName: "namespace-parts")]
        public IEnumerable<string> NamespaceParts { get; set; }

        [Option(longName: "type-parts")]
        public IEnumerable<string> TypeParts { get; set; }

        [Option(longName: "member-parts")]
        public IEnumerable<string> MemberParts { get; set; }

        [Option(longName: "preferred-culture", shortName: 'c')]
        public string PreferredCulture { get; set; }

        [Option(longName: "base-local-url")]
        public string BaseLocalUrl { get; set; }

        [Option(longName: "class-hierarchy", Default = DefaultValues.ClassHierarchy)]
        public bool ClassHierarchy { get; set; }

        [Option(longName: "include-containing-namespace", Default = DefaultValues.IncludeContainingNamespace)]
        public bool IncludeContainingNamespace { get; set; }

        [Option(longName: "place-system-namespace-first", Default = DefaultValues.PlaceSystemNamespaceFirst)]
        public bool PlaceSystemNamespaceFirst { get; set; }

        [Option(longName: "format-declaration-base-list", Default = DefaultValues.FormatDeclarationBaseList)]
        public bool FormatDeclarationBaseList { get; set; }

        [Option(longName: "format-declaration-constraints", Default = DefaultValues.FormatDeclarationConstraints)]
        public bool FormatDeclarationConstraints { get; set; }

        [Option(longName: "max-derived-types", Default = DefaultValues.MaxDerivedTypes)]
        public int MaxDerivedTypes { get; set; }

        [Option(longName: "indicate-obsolete", Default = DefaultValues.IndicateObsolete)]
        public bool IndicateObsolete { get; set; }

        [Option(longName: "indicate-inherited-member", Default = DefaultValues.IndicateInheritedMember)]
        public bool IndicateInheritedMember { get; set; }

        [Option(longName: "indicate-overridden-member", Default = DefaultValues.IndicateOverriddenMember)]
        public bool IndicateOverriddenMember { get; set; }

        [Option(longName: "indicate-interface-implementation", Default = DefaultValues.IndicateInterfaceImplementation)]
        public bool IndicateInterfaceImplementation { get; set; }

        [Option(longName: "include-constant-value", Default = DefaultValues.IncludeConstantValue)]
        public bool IncludeConstantValue { get; set; }

        [Option(longName: "include-attribute-arguments", Default = DefaultValues.IncludeAttributeArguments)]
        public bool IncludeAttributeArguments { get; set; }

        [Option(longName: "include-inherited-interface-members", Default = DefaultValues.IncludeInheritedInterfaceMembers)]
        public bool IncludeInheritedInterfaceMembers { get; set; }

        [Option(longName: "omit-ienumerable", Default = DefaultValues.OmitIEnumerable)]
        public bool OmitIEnumerable { get; set; }
    }
}
