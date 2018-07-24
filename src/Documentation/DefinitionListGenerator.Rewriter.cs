// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public static partial class DefinitionListGenerator
    {
        private class Rewriter : CSharpSyntaxRewriter
        {
            private static readonly SymbolDisplayFormat _enumFieldValueFormat = new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                memberOptions: SymbolDisplayMemberOptions.IncludeContainingType);

            private static readonly SyntaxAnnotation[] _simplifierAnnotationAsArray = new SyntaxAnnotation[] { Simplifier.Annotation };

            private ITypeSymbol _enumTypeSymbol;

            public Rewriter(SemanticModel semanticModel, CancellationToken cancellationToken = default)
            {
                SemanticModel = semanticModel;
                CancellationToken = cancellationToken;
            }

            public SemanticModel SemanticModel { get; }

            public CancellationToken CancellationToken { get; }

            public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
            {
                if (SemanticModel.GetSymbol(node.Left, CancellationToken)?.Kind == SymbolKind.Namespace)
                {
                    return node
                        .WithRight((SimpleNameSyntax)Visit(node.Right))
                        .WithAdditionalAnnotations(_simplifierAnnotationAsArray);
                }

                return base.VisitQualifiedName(node);
            }

            public override SyntaxNode VisitParameter(ParameterSyntax node)
            {
                EqualsValueClauseSyntax @default = node.Default;

                if (@default != null)
                {
                    ExpressionSyntax value = @default?.Value;

                    if (value != null)
                    {
                        ITypeSymbol typeSymbol = SemanticModel.GetTypeSymbol(node.Type, CancellationToken);

                        if (typeSymbol?.TypeKind == TypeKind.Enum)
                        {
                            node = (ParameterSyntax)base.VisitParameter(node);

                            try
                            {
                                _enumTypeSymbol = typeSymbol;
                                value = (ExpressionSyntax)Visit(value);
                            }
                            finally
                            {
                                _enumTypeSymbol = null;
                            }

                            return node.WithDefault(node.Default.WithValue(value));
                        }
                    }
                }

                return base.VisitParameter(node);
            }

            public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
            {
                if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression))
                {
                    ISymbol symbol = SemanticModel.GetSymbol(node, CancellationToken);

                    if (symbol?.Kind == SymbolKind.Field
                        && symbol.ContainingType?.TypeKind == TypeKind.Enum)
                    {
                        return node.WithAdditionalAnnotations(_simplifierAnnotationAsArray);
                    }
                }

                return base.VisitMemberAccessExpression(node);
            }

            public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
            {
                if (_enumTypeSymbol != null
                    && !node.IsParentKind(SyntaxKind.SimpleMemberAccessExpression))
                {
                    MemberAccessExpressionSyntax newNode = CSharpFactory.SimpleMemberAccessExpression(
                        SyntaxFactory.ParseExpression(_enumTypeSymbol.ToDisplayString(_enumFieldValueFormat)),
                        node);

                    return newNode.WithAdditionalAnnotations(_simplifierAnnotationAsArray);
                }

                return base.VisitIdentifierName(node);
            }

            public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
            {
                Debug.Assert(node.IsParentKind(SyntaxKind.EqualsValueClause)
                    && node.Parent.IsParentKind(SyntaxKind.Parameter), node.ToString());

                if (node.IsParentKind(SyntaxKind.EqualsValueClause)
                    && node.Parent.IsParentKind(SyntaxKind.Parameter))
                {
                    return CSharpFactory.DefaultLiteralExpression();
                }

                return base.VisitDefaultExpression(node);
            }
        }
    }
}
