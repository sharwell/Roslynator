// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Documentation.Markdown;
using Roslynator.Utilities;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static readonly UTF8Encoding _utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        [SuppressMessage("Redundancy", "RCS1163")]
        private static void Main(string[] args)
        {
            GenerateDocumentation(@"..\..\..\..\..\..\docs\api\", "Roslynator API", "Roslynator.CSharp.dll", "Roslynator.CSharp.Workspaces.dll");
            GenerateDocumentation(@"..\..\..\..\..\..\docs\apitest\", "Foo API", "Roslynator.Documentation.DocumentationTest.dll");
        }

        private static void GenerateDocumentation(string directoryPath, string heading, params string[] assemblyNames)
        {
            CompilationDocumentationInfo compilationInfo = CreateFromTrustedPlatformAssemblies(assemblyNames);

            var options = new DocumentationOptions(
                parts: DocumentationParts.All,
                formatBaseList: true,
                formatConstraints: true);

            var generator = new MarkdownDocumentationGenerator(compilationInfo, DocumentationUriProvider.GitHub, options);

            var builder = new SymbolDefinitionListBuilder();

            builder.AppendSymbols(compilationInfo.Types.Where(f => f.ContainingType == null));

            string content = builder.ToString();

            string content2 = M(content, compilationInfo.Compilation.ExternalReferences).Result;

            FileHelper.WriteAllText(directoryPath + "_api.cs", content2, Encoding.UTF8, onlyIfChanges: true, fileMustExists: false);

            foreach (DocumentationGeneratorResult result in generator.Generate(
                heading,
                objectModelHeading: heading + " Object Model",
                extendedExternalTypesHeading: "External Types Extended by " + heading))
            {
                string path = directoryPath + result.Path;

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                FileHelper.WriteAllText(path, result.Content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
            }
        }

        internal static CompilationDocumentationInfo CreateFromTrustedPlatformAssemblies(string[] assemblyNames)
        {
            ImmutableDictionary<string, string> paths = AppContext
                .GetData("TRUSTED_PLATFORM_ASSEMBLIES")
                .ToString()
                .Split(';')
                .ToImmutableDictionary(Path.GetFileName, StringComparer.OrdinalIgnoreCase);

            List<PortableExecutableReference> references = assemblyNames
                .Select(f => MetadataReference.CreateFromFile(paths[f]))
                .ToList();

            IEnumerable<PortableExecutableReference> compilationReferences = paths
                .Values
                .Where(path => !references.Any(reference => reference.FilePath == path))
                .Select(f => MetadataReference.CreateFromFile(f))
                .Concat(references);

            CSharpCompilation compilation = CSharpCompilation.Create(
                "",
                syntaxTrees: default(IEnumerable<SyntaxTree>),
                references: compilationReferences,
                options: default(CSharpCompilationOptions));

            return new CompilationDocumentationInfo(
                compilation,
                references.Select(f => (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(f)));
        }

        private static async Task<string> M(string source, IEnumerable<MetadataReference> references)
        {
            Project project = new AdhocWorkspace()
                .CurrentSolution
                .AddProject("AdHocProject", "AdHocProject", LanguageNames.CSharp)
                .WithMetadataReferences(references);

            var parseOptions = (CSharpParseOptions)project.ParseOptions;

            Document document = project
                .WithParseOptions(parseOptions.WithLanguageVersion(LanguageVersion.Latest))
                .AddDocument("AdHocFile.cs", SourceText.From(source));

            SemanticModel semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);
            SyntaxNode root = await document.GetSyntaxRootAsync().ConfigureAwait(false);

            var rewriter = new Rewriter(semanticModel);

            root = rewriter.Visit(root);

            document = document.WithSyntaxRoot(root);

            document = await Simplifier.ReduceAsync(document).ConfigureAwait(false);

            root = await document.GetSyntaxRootAsync().ConfigureAwait(false);

            return root.ToFullString();
        }

        private class Rewriter : CSharpSyntaxRewriter
        {
            public Rewriter(SemanticModel semanticModel)
            {
                SemanticModel = semanticModel;
            }

            public SemanticModel SemanticModel { get; }

            public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
            {
                if (SemanticModel.GetSymbol(node.Left)?.Kind == SymbolKind.Namespace)
                {
                    return node
                        .WithRight((SimpleNameSyntax)Visit(node.Right))
                        .WithAdditionalAnnotations(Simplifier.Annotation);
                }

                return base.VisitQualifiedName(node);
            }
        }
    }
}
