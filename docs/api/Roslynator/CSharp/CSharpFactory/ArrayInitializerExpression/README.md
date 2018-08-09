# CSharpFactory\.ArrayInitializerExpression Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [ArrayInitializerExpression(SeparatedSyntaxList\<ExpressionSyntax>)](#Roslynator_CSharp_CSharpFactory_ArrayInitializerExpression_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__) | |
| [ArrayInitializerExpression(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken)](#Roslynator_CSharp_CSharpFactory_ArrayInitializerExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__Microsoft_CodeAnalysis_SyntaxToken_) | |

## ArrayInitializerExpression\(SeparatedSyntaxList\<ExpressionSyntax>\)<a name="Roslynator_CSharp_CSharpFactory_ArrayInitializerExpression_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__"></a>

```csharp
public static InitializerExpressionSyntax ArrayInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expressions | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)

## ArrayInitializerExpression\(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken\)<a name="Roslynator_CSharp_CSharpFactory_ArrayInitializerExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__Microsoft_CodeAnalysis_SyntaxToken_"></a>

```csharp
public static InitializerExpressionSyntax ArrayInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| openBraceToken | |
| expressions | |
| closeBraceToken | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)

