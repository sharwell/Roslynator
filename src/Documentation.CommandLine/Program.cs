// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Roslynator.Documentation.Markdown;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ParserResult<CommandLineOptions> result = Parser.Default.ParseArguments<CommandLineOptions>(args);

            if (!(result is Parsed<CommandLineOptions> parsed))
                return;

            CommandLineOptions options = parsed.Value;

            Encoding encoding = GetEncoding();

            if (encoding == null)
                return;

            if (options.MaxDerivedItems < 0)
            {
                Console.WriteLine("Maximum number of derived items must be equal or greater than 0.");
                return;
            }

            IEnumerable<string> assemblyReferences = GetAssemblyReferences(options.AssemblyReferences);

            if (assemblyReferences == null)
                return;

            List<PortableExecutableReference> references = assemblyReferences
                .Select(f => MetadataReference.CreateFromFile(f))
                .ToList();

            foreach (string assemblyPath in options.Assemblies)
            {
                if (!TryGetReference(references, assemblyPath, out PortableExecutableReference reference))
                {
                    if (File.Exists(assemblyPath))
                    {
                        reference = MetadataReference.CreateFromFile(assemblyPath);
                        references.Add(reference);
                    }
                    else
                    {
                        Console.WriteLine($"Assembly '{assemblyPath}' not found.");
                    }
                }
            }

            CSharpCompilation compilation = CSharpCompilation.Create(
                "",
                syntaxTrees: default(IEnumerable<SyntaxTree>),
                references: references,
                options: default(CSharpCompilationOptions));

            var documentationModel = new DocumentationModel(
                compilation,
                options.Assemblies.Select(assemblyPath =>
                {
                    TryGetReference(references, assemblyPath, out PortableExecutableReference reference);
                    return (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);
                }));

            if (!Enum.TryParse(options.Depth, ignoreCase: true, out DocumentationDepth depth))
                depth = DocumentationOptions.DefaultValues.Depth;

            if (!TryGetNamespaceDocumentationParts(options.NamespaceParts, out NamespaceDocumentationParts namespaceParts))
                return;

            if (!TryGetTypeDocumentationParts(options.TypeParts, out TypeDocumentationParts typeParts))
                return;

            if (!TryGetMemberDocumentationParts(options.MemberParts, out MemberDocumentationParts memberParts))
                return;

            var documentationOptions = new DocumentationOptions(
                preferredCultureName: options.PreferredCulture,
                baseLocalUrl: options.BaseLocalUrl,
                depth: depth,
                namespaceParts: namespaceParts,
                typeParts: typeParts,
                memberParts: memberParts,
                maxDerivedItems: options.MaxDerivedItems,
                systemNamespaceFirst: options.SystemNamespaceFirst,
                formatDefinitionBaseList: options.FormatDefinitionBaseList,
                formatDefinitionConstraints: options.FormatDefinitionConstraints,
                indicateObsolete: options.IndicateObsolete,
                indicateInheritedMember: options.IndicateInheritedMember,
                indicateOverriddenMember: options.IndicateOverriddenMember,
                indicateInterfaceImplementation: options.IndicateInterfaceImplementation,
                attributeArguments: options.AttributeArguments,
                inheritedInterfaceMembers: options.InheritedInterfaceMembers,
                omitIEnumerable: options.OmitIEnumerable);

            var generator = new MarkdownDocumentationGenerator(documentationModel, DocumentationUrlProvider.GitHub, documentationOptions);

            string directoryPath = options.OutputDirectory;

            Console.WriteLine($"Documentation is being generated to '{options.OutputDirectory}'.");

            foreach (DocumentationGeneratorResult documentationFile in generator.Generate(heading: options.Heading))
            {
                string path = Path.Combine(directoryPath, documentationFile.Path);

#if DEBUG
                Console.WriteLine($"saving '{path}'");
#else
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, documentationFile.Content, encoding);
#endif
            }

            Console.WriteLine($"Documentation successfully generated to '{options.OutputDirectory}'.");

            Encoding GetEncoding()
            {
                if (string.Equals(options.Mode, "github", StringComparison.OrdinalIgnoreCase))
                    return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

                Console.WriteLine($"Unknown mode '{options.Mode}'.");
                return null;
            }
        }

        private static IEnumerable<string> GetAssemblyReferences(string assemblyReferences)
        {
            if (assemblyReferences.Contains(";"))
            {
                return assemblyReferences.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                string path = assemblyReferences;

                if (!File.Exists(path))
                {
                    Console.WriteLine($"File not found '{path}'.");
                    return null;
                }

                string extension = Path.GetExtension(path);

                if (string.Equals(extension, ".dll", StringComparison.OrdinalIgnoreCase))
                {
                    return assemblyReferences.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return File.ReadLines(assemblyReferences).Where(f => !string.IsNullOrWhiteSpace(f));
                }
            }
        }

        private static bool TryGetNamespaceDocumentationParts(IEnumerable<string> values, out NamespaceDocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.NamespaceParts;
                return true;
            }

            parts = NamespaceDocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(value, ignoreCase: true, out NamespaceDocumentationParts result))
                {
                    parts |= result;
                }
                else
                {
                    Console.WriteLine($"Unknown namespace documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetTypeDocumentationParts(IEnumerable<string> values, out TypeDocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.TypeParts;
                return true;
            }

            parts = TypeDocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(value, ignoreCase: true, out TypeDocumentationParts result))
                {
                    parts |= result;
                }
                else
                {
                    Console.WriteLine($"Unknown type documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetMemberDocumentationParts(IEnumerable<string> values, out MemberDocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.MemberParts;
                return true;
            }

            parts = MemberDocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(value, ignoreCase: true, out MemberDocumentationParts result))
                {
                    parts |= result;
                }
                else
                {
                    Console.WriteLine($"Unknown member documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetReference(List<PortableExecutableReference> references, string path, out PortableExecutableReference reference)
        {
            if (path.Contains(Path.DirectorySeparatorChar))
            {
                foreach (PortableExecutableReference r in references)
                {
                    if (r.FilePath == path)
                    {
                        reference = r;
                        return true;
                    }
                }
            }
            else
            {
                foreach (PortableExecutableReference r in references)
                {
                    string filePath = r.FilePath;

                    int index = filePath.LastIndexOf(Path.DirectorySeparatorChar);

                    if (string.Compare(filePath, index + 1, path, 0, path.Length, StringComparison.Ordinal) == 0)
                    {
                        reference = r;
                        return true;
                    }
                }
            }

            reference = null;
            return false;
        }
    }
}
