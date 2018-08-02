// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CodeFixes;
using Roslynator.CSharp.SyntaxRewriters;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CSharp.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseAsyncAwaitCodeFixProvider))]
    [Shared]
    public class UseAsyncAwaitCodeFixProvider : BaseCodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.UseAsyncAwait); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindFirstAncestorOrSelf(root, context.Span, out ReturnStatementSyntax returnStatement))
                return;

            Diagnostic diagnostic = context.Diagnostics[0];

            CodeAction codeAction = CodeAction.Create(
                "Use async/await",
                ct => RefactorAsync(context.Document, returnStatement, ct),
                GetEquivalenceKey(diagnostic.Id));

            context.RegisterCodeFix(codeAction, diagnostic);
        }

        private static Task<Document> RefactorAsync(
            Document document,
            ReturnStatementSyntax returnStatement,
            CancellationToken cancellationToken)
        {
            for (SyntaxNode node = returnStatement.Parent; node != null; node = node.Parent)
            {
                switch (node.Kind())
                {
                    case SyntaxKind.MethodDeclaration:
                        {
                            var methodDeclaration = (MethodDeclarationSyntax)node;

                            var newNode = (MethodDeclarationSyntax)UseAsyncAwaitRewriter.Instance.VisitMethodDeclaration(methodDeclaration);

                            newNode = ModifierList<MethodDeclarationSyntax>.Instance.Insert(newNode, SyntaxKind.AsyncKeyword);

                            return document.ReplaceNodeAsync(methodDeclaration, newNode, cancellationToken);
                        }
                    case SyntaxKind.OperatorDeclaration:
                        {
                            var operatorDeclaration = (OperatorDeclarationSyntax)node;

                            var newNode = (OperatorDeclarationSyntax)UseAsyncAwaitRewriter.Instance.VisitOperatorDeclaration(operatorDeclaration);

                            newNode = ModifierList<OperatorDeclarationSyntax>.Instance.Insert(newNode, SyntaxKind.AsyncKeyword);

                            return document.ReplaceNodeAsync(operatorDeclaration, newNode, cancellationToken);
                        }
                    case SyntaxKind.ConversionOperatorDeclaration:
                        {
                            var operatorDeclaration = (ConversionOperatorDeclarationSyntax)node;

                            var newNode = (ConversionOperatorDeclarationSyntax)UseAsyncAwaitRewriter.Instance.VisitConversionOperatorDeclaration(operatorDeclaration);

                            newNode = ModifierList<ConversionOperatorDeclarationSyntax>.Instance.Insert(newNode, SyntaxKind.AsyncKeyword);

                            return document.ReplaceNodeAsync(operatorDeclaration, newNode, cancellationToken);
                        }
                    case SyntaxKind.LocalFunctionStatement:
                        {
                            var localFunction = (LocalFunctionStatementSyntax)node;

                            var newBody = (BlockSyntax)UseAsyncAwaitRewriter.Instance.VisitBlock(localFunction.Body);

                            LocalFunctionStatementSyntax newNode = localFunction.WithBody(newBody);

                            newNode = ModifierList<LocalFunctionStatementSyntax>.Instance.Insert(newNode, SyntaxKind.AsyncKeyword);

                            return document.ReplaceNodeAsync(localFunction, newNode, cancellationToken);
                        }
                    case SyntaxKind.SimpleLambdaExpression:
                        {
                            var lambda = (SimpleLambdaExpressionSyntax)node;

                            var newBody = (BlockSyntax)UseAsyncAwaitRewriter.Instance.VisitBlock((BlockSyntax)lambda.Body);

                            SimpleLambdaExpressionSyntax newNode = lambda
                                .WithBody(newBody)
                                .WithAsyncKeyword(AsyncKeyword());

                            return document.ReplaceNodeAsync(lambda, newNode, cancellationToken);
                        }
                    case SyntaxKind.ParenthesizedLambdaExpression:
                        {
                            var lambda = (ParenthesizedLambdaExpressionSyntax)node;

                            var newBody = (BlockSyntax)UseAsyncAwaitRewriter.Instance.VisitBlock((BlockSyntax)lambda.Body);

                            ParenthesizedLambdaExpressionSyntax newNode = lambda
                                .WithBody(newBody)
                                .WithAsyncKeyword(AsyncKeyword());

                            return document.ReplaceNodeAsync(lambda, newNode, cancellationToken);
                        }
                    case SyntaxKind.AnonymousMethodExpression:
                        {
                            var anonymousMethod = (AnonymousMethodExpressionSyntax)node;

                            var newBody = (BlockSyntax)UseAsyncAwaitRewriter.Instance.VisitBlock((BlockSyntax)anonymousMethod.Body);

                            AnonymousMethodExpressionSyntax newNode = anonymousMethod
                                .WithBody(newBody)
                                .WithAsyncKeyword(AsyncKeyword());

                            return document.ReplaceNodeAsync(anonymousMethod, newNode, cancellationToken);
                        }
                }
            }

            throw new InvalidOperationException();
        }

        private class UseAsyncAwaitRewriter : SkipFunctionRewriter
        {
            public static UseAsyncAwaitRewriter Instance { get; } = new UseAsyncAwaitRewriter();

            public override SyntaxNode VisitReturnStatement(ReturnStatementSyntax node)
            {
                ExpressionSyntax expression = node.Expression;

                if (expression?.IsKind(SyntaxKind.AwaitExpression) == false)
                {
                    return node.WithExpression(SyntaxFactory.AwaitExpression(expression.WithoutTrivia()).WithTriviaFrom(expression));
                }

                return base.VisitReturnStatement(node);
            }
        }
    }
}
