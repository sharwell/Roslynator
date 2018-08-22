﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

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
            Parser.Default.ParseArguments<DocumentationCommandLineOptions, DeclarationListCommandLineOptions>(args)
                .MapResult(
                  (DocumentationCommandLineOptions options) => GenerateDocumentation(options),
                  (DeclarationListCommandLineOptions options) => GenerateDeclarationList(options),
                  _ => 1);
        }

        private static int GenerateDocumentation(DocumentationCommandLineOptions options)
        {
            Encoding encoding = GetEncoding();

            if (encoding == null)
                return 1;

            if (options.MaxDerivedTypes < 0)
            {
                Console.WriteLine("Maximum number of derived items must be equal or greater than 0.");
                return 1;
            }

            if (!TryGetNamespaceDocumentationParts(options.NamespaceParts, out NamespaceDocumentationParts namespaceParts))
                return 1;

            if (!TryGetTypeDocumentationParts(options.TypeParts, out TypeDocumentationParts typeParts))
                return 1;

            if (!TryGetMemberDocumentationParts(options.MemberParts, out MemberDocumentationParts memberParts))
                return 1;

            DocumentationModel documentationModel = CreateDocumentationModel(options.AssemblyReferences, options.Assemblies, options.AdditionalXmlDocumentation);

            if (documentationModel == null)
                return 1;

            var documentationOptions = new DocumentationOptions(
                preferredCultureName: options.PreferredCulture,
                baseLocalUrl: options.BaseLocalUrl,
                depth: options.Depth,
                namespaceParts: namespaceParts,
                typeParts: typeParts,
                memberParts: memberParts,
                maxDerivedTypes: options.MaxDerivedTypes,
                classHierarchy: options.ClassHierarchy,
                includeContainingNamespace: options.IncludeContainingNamespace,
                placeSystemNamespaceFirst: options.PlaceSystemNamespaceFirst,
                formatDeclarationBaseList: options.FormatDeclarationBaseList,
                formatDeclarationConstraints: options.FormatDeclarationConstraints,
                markObsolete: options.MarkObsolete,
                includeMemberInheritedFrom: options.IncludeMemberInheritedFrom,
                includeMemberOverrides: options.IncludeMemberOverrides,
                includeMemberImplements: options.IncludeMemberImplements,
                includeMemberConstantValue: options.IncludeMemberConstantValue,
                includeAttributeArguments: options.IncludeAttributeArguments,
                includeInheritedInterfaceMembers: options.IncludeInheritedInterfaceMembers,
                omitIEnumerable: options.OmitIEnumerable);

            var generator = new MarkdownDocumentationGenerator(documentationModel, WellKnownDocumentationUrlProviders.GitHub, documentationOptions);

            string directoryPath = options.OutputDirectory;

            Console.WriteLine($"Documentation is being generated to '{options.OutputDirectory}'.");

            foreach (DocumentationGeneratorResult documentationFile in generator.Generate(heading: options.Heading))
            {
                string path = Path.Combine(directoryPath, documentationFile.FilePath);

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

            return 0;
        }

        private static int GenerateDeclarationList(DeclarationListCommandLineOptions options)
        {
            DocumentationModel documentationModel = CreateDocumentationModel(options.AssemblyReferences, options.Assemblies, options.AdditionalXmlDocumentation);

            if (documentationModel == null)
                return 1;

            var declarationListOptions = new DeclarationListOptions(
                indent: options.Indent,
                indentChars: options.IndentChars,
                newLineBeforeOpenBrace: options.NewLineBeforeOpenBrace,
                emptyLineBetweenMembers: options.EmptyLineBetweenMembers,
                splitAttributes: options.SplitAttributes,
                includeAttributeArguments: options.IncludeAttributeArguments,
                omitIEnumerable: options.OmitIEnumerable,
                useDefaultLiteral: options.UseDefaultLiteral);

            Console.WriteLine($"Declaration list is being generated to '{options.OutputPath}'.");

            string content = DeclarationListGenerator.GenerateAsync(documentationModel, declarationListOptions).Result;

            string path = options.OutputPath;

#if DEBUG
            Console.WriteLine($"saving '{path}'");
#else
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, content, Encoding.UTF8);
#endif

            Console.WriteLine($"Declaration list successfully generated to '{options.OutputPath}'.");

            return 0;
        }

        private static DocumentationModel CreateDocumentationModel(string assemblyReferencesValue, IEnumerable<string> assemblies, IEnumerable<string> additionalXmlDocumentationPaths)
        {
            IEnumerable<string> assemblyReferences = GetAssemblyReferences(assemblyReferencesValue);

            if (assemblyReferences == null)
                return null;

            List<PortableExecutableReference> references = assemblyReferences
                .Select(f => MetadataReference.CreateFromFile(f))
                .ToList();

            foreach (string assemblyPath in assemblies)
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

            return new DocumentationModel(
                compilation,
                assemblies.Select(assemblyPath =>
                {
                    TryGetReference(references, assemblyPath, out PortableExecutableReference reference);
                    return (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);
                }),
                additionalXmlDocumentationPaths: additionalXmlDocumentationPaths);
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
