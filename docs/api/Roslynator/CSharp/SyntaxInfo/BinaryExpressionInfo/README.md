<a name="_Top"></a>

# SyntaxInfo\.BinaryExpressionInfo Method

[Home](../../../../README.md#_Top)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxInfo](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [BinaryExpressionInfo(BinaryExpressionSyntax, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_) | Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md#_Top) from the specified binary expression\. |
| [BinaryExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md#_Top) from the specified node\. |

## BinaryExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md#_Top) from the specified binary expression\.

```csharp
public static BinaryExpressionInfo BinaryExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### binaryExpression

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md#_Top)

## BinaryExpressionInfo\(SyntaxNode, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md#_Top) from the specified node\.

```csharp
public static BinaryExpressionInfo BinaryExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### node

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md#_Top)

