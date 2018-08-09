# CSharpFactory\.UncheckedExpression Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [UncheckedExpression(ExpressionSyntax)](#Roslynator_CSharp_CSharpFactory_UncheckedExpression_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_) | |
| [UncheckedExpression(SyntaxToken, ExpressionSyntax, SyntaxToken)](#Roslynator_CSharp_CSharpFactory_UncheckedExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_Microsoft_CodeAnalysis_SyntaxToken_) | |

## UncheckedExpression\(ExpressionSyntax\)<a name="Roslynator_CSharp_CSharpFactory_UncheckedExpression_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_"></a>

```csharp
public static CheckedExpressionSyntax UncheckedExpression(ExpressionSyntax expression)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expression | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)

## UncheckedExpression\(SyntaxToken, ExpressionSyntax, SyntaxToken\)<a name="Roslynator_CSharp_CSharpFactory_UncheckedExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_Microsoft_CodeAnalysis_SyntaxToken_"></a>

```csharp
public static CheckedExpressionSyntax UncheckedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| openParenToken | |
| expression | |
| closeParenToken | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)

