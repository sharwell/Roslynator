# SyntaxInfo\.SimpleMemberInvocationExpressionInfo Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxInfo](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [SimpleMemberInvocationExpressionInfo(InvocationExpressionSyntax, Boolean)](#Roslynator_CSharp_SyntaxInfo_SimpleMemberInvocationExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_InvocationExpressionSyntax_System_Boolean_) | Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md#_Top) from the specified invocation expression\. |
| [SimpleMemberInvocationExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_SimpleMemberInvocationExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md#_Top) from the specified node\. |

## SimpleMemberInvocationExpressionInfo\(InvocationExpressionSyntax, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_SimpleMemberInvocationExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_InvocationExpressionSyntax_System_Boolean_"></a>

### Summary

Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md#_Top) from the specified invocation expression\.

```csharp
public static SimpleMemberInvocationExpressionInfo SimpleMemberInvocationExpressionInfo(InvocationExpressionSyntax invocationExpression, bool allowMissing = false)
```

### Parameters

#### invocationExpression

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md#_Top)

## SimpleMemberInvocationExpressionInfo\(SyntaxNode, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_SimpleMemberInvocationExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md#_Top) from the specified node\.

```csharp
public static SimpleMemberInvocationExpressionInfo SimpleMemberInvocationExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### node

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md#_Top)

