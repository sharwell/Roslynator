# SyntaxInfo\.SingleParameterLambdaExpressionInfo Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [SingleParameterLambdaExpressionInfo(LambdaExpressionSyntax, Boolean)](#Roslynator_CSharp_SyntaxInfo_SingleParameterLambdaExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_LambdaExpressionSyntax_System_Boolean_) | Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified lambda expression\. |
| [SingleParameterLambdaExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_SingleParameterLambdaExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified node\. |

## SingleParameterLambdaExpressionInfo\(LambdaExpressionSyntax, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_SingleParameterLambdaExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified lambda expression\.

```csharp
public static SingleParameterLambdaExpressionInfo SingleParameterLambdaExpressionInfo(LambdaExpressionSyntax lambdaExpression, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| lambdaExpression | |
| allowMissing | |

#### Returns

[SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md)

## SingleParameterLambdaExpressionInfo\(SyntaxNode, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_SingleParameterLambdaExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified node\.

```csharp
public static SingleParameterLambdaExpressionInfo SingleParameterLambdaExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md)

