// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.Documentation
{
    public static partial class DefinitionListGenerator
    {
        public static Task<string> GenerateAsync(DocumentationModel documentationModel)
        {
            var builder = new DefinitionListBuilder();

            builder.AppendSymbols(documentationModel.Types.Where(f => f.ContainingType == null));

            string content = builder.ToString();

            return PostProcess(content);

            async Task<string> PostProcess(string source)
            {
                Project project = new AdhocWorkspace()
                    .CurrentSolution
                    .AddProject("AdHocProject", "AdHocProject", LanguageNames.CSharp)
                    .WithMetadataReferences(documentationModel.Compilation.References);

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
        }
    }
}
