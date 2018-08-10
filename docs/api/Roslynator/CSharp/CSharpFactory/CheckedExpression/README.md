# CSharpFactory\.CheckedExpression Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [CheckedExpression(ExpressionSyntax)](#Roslynator_CSharp_CSharpFactory_CheckedExpression_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_) | |
| [CheckedExpression(SyntaxToken, ExpressionSyntax, SyntaxToken)](#Roslynator_CSharp_CSharpFactory_CheckedExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_Microsoft_CodeAnalysis_SyntaxToken_) | |

## CheckedExpression\(ExpressionSyntax\)<a name="Roslynator_CSharp_CSharpFactory_CheckedExpression_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_"></a>

```csharp
public static CheckedExpressionSyntax CheckedExpression(ExpressionSyntax expression)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expression | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)

## CheckedExpression\(SyntaxToken, ExpressionSyntax, SyntaxToken\)<a name="Roslynator_CSharp_CSharpFactory_CheckedExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_Microsoft_CodeAnalysis_SyntaxToken_"></a>

```csharp
public static CheckedExpressionSyntax CheckedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| openParenToken | |
| expression | |
| closeParenToken | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)
