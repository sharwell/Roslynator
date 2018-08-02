// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseAsyncAwaitAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.UseAsyncAwait); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzeReturnStatement, SyntaxKind.ReturnStatement);
        }

        public static void AnalyzeReturnStatement(SyntaxNodeAnalysisContext context)
        {
            var returnStatement = (ReturnStatementSyntax)context.Node;

            ExpressionSyntax returnExpression = returnStatement.Expression;

            if (!returnExpression.IsKind(SyntaxKind.InvocationExpression))
                return;

            var invocationExpression = (InvocationExpressionSyntax)returnExpression;

            if (GetSimpleName()?.Identifier.ValueText.EndsWith("Async", StringComparison.Ordinal) != true)
                return;

            SyntaxNode usingStatement = FindContainingUsingStatement(returnStatement);

            if (usingStatement == null)
                return;

            if (!AnalyzeContainingMethod(usingStatement))
                return;

            context.ReportDiagnostic(DiagnosticDescriptors.UseAsyncAwait, returnExpression);

            SimpleNameSyntax GetSimpleName()
            {
                ExpressionSyntax expression = invocationExpression.Expression;

                switch (expression.Kind())
                {
                    case SyntaxKind.SimpleMemberAccessExpression:
                        return ((MemberAccessExpressionSyntax)expression).Name;
                    case SyntaxKind.IdentifierName:
                    case SyntaxKind.GenericName:
                        return (SimpleNameSyntax)expression;
                    default:
                        return null;
                }
            }

            SyntaxNode FindContainingUsingStatement(SyntaxNode node)
            {
                for (node = node.Parent; node != null; node = node.Parent)
                {
                    switch (node.Kind())
                    {
                        case SyntaxKind.UsingStatement:
                            {
                                return node;
                            }
                        case SyntaxKind.FieldDeclaration:
                        case SyntaxKind.EventFieldDeclaration:
                        case SyntaxKind.MethodDeclaration:
                        case SyntaxKind.OperatorDeclaration:
                        case SyntaxKind.ConversionOperatorDeclaration:
                        case SyntaxKind.ConstructorDeclaration:
                        case SyntaxKind.DestructorDeclaration:
                        case SyntaxKind.PropertyDeclaration:
                        case SyntaxKind.EventDeclaration:
                        case SyntaxKind.IndexerDeclaration:
                        case SyntaxKind.LocalFunctionStatement:
                        case SyntaxKind.SimpleLambdaExpression:
                        case SyntaxKind.ParenthesizedLambdaExpression:
                        case SyntaxKind.AnonymousMethodExpression:
                            {
                                return null;
                            }
                        default:
                            {
                                Debug.Assert(!(node is MemberDeclarationSyntax), node.Kind().ToString());
                                break;
                            }
                    }
                }

                Debug.Fail("");

                return null;
            }

            bool AnalyzeContainingMethod(SyntaxNode node)
            {
                for (node = node.Parent; node != null; node = node.Parent)
                {
                    switch (node.Kind())
                    {
                        case SyntaxKind.MethodDeclaration:
                            {
                                var methodDeclaration = (MethodDeclarationSyntax)node;

                                if (methodDeclaration.Modifiers.Contains(SyntaxKind.AsyncKeyword))
                                    return false;

                                ITypeSymbol typeSymbol = context.SemanticModel.GetTypeSymbol(methodDeclaration.ReturnType, context.CancellationToken);

                                return IsTaskLike(typeSymbol);
                            }
                        case SyntaxKind.LocalFunctionStatement:
                            {
                                var localFunction = (LocalFunctionStatementSyntax)node;

                                if (localFunction.Modifiers.Contains(SyntaxKind.AsyncKeyword))
                                    return false;

                                ITypeSymbol typeSymbol = context.SemanticModel.GetTypeSymbol(localFunction.ReturnType, context.CancellationToken);

                                return IsTaskLike(typeSymbol);
                            }
                        case SyntaxKind.SimpleLambdaExpression:
                        case SyntaxKind.ParenthesizedLambdaExpression:
                            {
                                var lambda = (LambdaExpressionSyntax)node;

                                if (lambda.AsyncKeyword.IsKind(SyntaxKind.AsyncKeyword))
                                    return false;

                                if (!(context.SemanticModel.GetSymbol(lambda, context.CancellationToken) is IMethodSymbol methodSymbol))
                                    return false;

                                return IsTaskLike(methodSymbol.ReturnType);
                            }
                        case SyntaxKind.AnonymousMethodExpression:
                            {
                                var anonymousMethod = (AnonymousMethodExpressionSyntax)node;

                                if (anonymousMethod.AsyncKeyword.IsKind(SyntaxKind.AsyncKeyword))
                                    return false;

                                if (!(context.SemanticModel.GetSymbol(anonymousMethod, context.CancellationToken) is IMethodSymbol methodSymbol))
                                    return false;

                                return IsTaskLike(methodSymbol.ReturnType);
                            }
                        case SyntaxKind.OperatorDeclaration:
                        case SyntaxKind.ConversionOperatorDeclaration:
                        case SyntaxKind.FieldDeclaration:
                        case SyntaxKind.EventFieldDeclaration:
                        case SyntaxKind.ConstructorDeclaration:
                        case SyntaxKind.DestructorDeclaration:
                        case SyntaxKind.PropertyDeclaration:
                        case SyntaxKind.EventDeclaration:
                        case SyntaxKind.IndexerDeclaration:
                            {
                                return false;
                            }
                    }
                }

                Debug.Fail("");

                return false;
            }

            bool IsTaskLike(ITypeSymbol typeSymbol)
            {
                if (typeSymbol?.IsErrorType() == false)
                {
                    ITypeSymbol originalDefinition = typeSymbol.OriginalDefinition;

                    if (originalDefinition.Name == "ValueTask`1"
                        && originalDefinition.ContainingNamespace.HasMetadataName(MetadataNames.System_Threading_Tasks))
                    {
                        return true;
                    }

                    if (originalDefinition.EqualsOrInheritsFrom(MetadataNames.System_Threading_Tasks_Task_T))
                        return true;
                }

                return false;
            }
        }
    }
}
