<a name="_Top"></a>

# SyntaxInfo\.AsExpressionInfo Method

[Home](../../../../README.md#_Top)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxInfo](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [AsExpressionInfo(BinaryExpressionSyntax, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_) | Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md#_Top) from the specified binary expression\. |
| [AsExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md#_Top) from the specified node\. |

## AsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md#_Top) from the specified binary expression\.

```csharp
public static AsExpressionInfo AsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### binaryExpression

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md#_Top)

## AsExpressionInfo\(SyntaxNode, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md#_Top) from the specified node\.

```csharp
public static AsExpressionInfo AsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### node

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md#_Top)

