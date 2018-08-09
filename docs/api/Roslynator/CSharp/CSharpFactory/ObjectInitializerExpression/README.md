# CSharpFactory\.ObjectInitializerExpression Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [ObjectInitializerExpression(SeparatedSyntaxList\<ExpressionSyntax>)](#Roslynator_CSharp_CSharpFactory_ObjectInitializerExpression_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__) | |
| [ObjectInitializerExpression(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken)](#Roslynator_CSharp_CSharpFactory_ObjectInitializerExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__Microsoft_CodeAnalysis_SyntaxToken_) | |

## ObjectInitializerExpression\(SeparatedSyntaxList\<ExpressionSyntax>\)<a name="Roslynator_CSharp_CSharpFactory_ObjectInitializerExpression_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__"></a>

```csharp
public static InitializerExpressionSyntax ObjectInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expressions | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)

## ObjectInitializerExpression\(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken\)<a name="Roslynator_CSharp_CSharpFactory_ObjectInitializerExpression_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_SeparatedSyntaxList_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax__Microsoft_CodeAnalysis_SyntaxToken_"></a>

```csharp
public static InitializerExpressionSyntax ObjectInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| openBraceToken | |
| expressions | |
| closeBraceToken | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)

