// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp.Refactorings
{
    internal static class WhileStatementRefactoring
    {
        public static void ComputeRefactorings(RefactoringContext context, WhileStatementSyntax whileStatement)
        {
            SyntaxToken whileKeyword = whileStatement.WhileKeyword;

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ReplaceWhileWithDo)
                && whileKeyword.Span.Contains(context.Span))
            {
                context.RegisterRefactoring(
                    "Replace while with do",
                    cancellationToken => ReplaceWhileWithDoRefactoring.RefactorAsync(context.Document, whileStatement, cancellationToken),
                    RefactoringIdentifiers.ReplaceWhileWithDo);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ReplaceWhileWithIfAndDo)
                && whileKeyword.Span.Contains(context.Span)
                && whileStatement.Condition?.IsKind(SyntaxKind.TrueLiteralExpression) == false)
            {
                context.RegisterRefactoring(
                    "Replace while with if + do",
                    cancellationToken => ReplaceWhileWithIfAndDoRefactoring.RefactorAsync(context.Document, whileStatement, cancellationToken),
                    RefactoringIdentifiers.ReplaceWhileWithIfAndDo);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ReplaceWhileWithFor)
                && whileKeyword.Span.Contains(context.Span))
            {
                context.RegisterRefactoring(
                    ReplaceWhileWithForRefactoring.Title,
                    cancellationToken => ReplaceWhileWithForRefactoring.RefactorAsync(context.Document, whileStatement, cancellationToken),
                    RefactoringIdentifiers.ReplaceWhileWithFor);
            }
        }
    }
}