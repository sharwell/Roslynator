# SyntaxInfo\.AssignmentExpressionInfo Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxInfo](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [AssignmentExpressionInfo(AssignmentExpressionSyntax, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_AssignmentExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_AssignmentExpressionSyntax_System_Boolean_System_Boolean_) | Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md#_Top) from the specified assignment expression\. |
| [AssignmentExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_AssignmentExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md#_Top) from the specified node\. |

## AssignmentExpressionInfo\(AssignmentExpressionSyntax, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_AssignmentExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_AssignmentExpressionSyntax_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md#_Top) from the specified assignment expression\.

```csharp
public static AssignmentExpressionInfo AssignmentExpressionInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### assignmentExpression

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md#_Top)

## AssignmentExpressionInfo\(SyntaxNode, Boolean, Boolean\) <a name="Roslynator_CSharp_SyntaxInfo_AssignmentExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md#_Top) from the specified node\.

```csharp
public static AssignmentExpressionInfo AssignmentExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

### Parameters

#### node

#### walkDownParentheses

#### allowMissing

### Returns

Roslynator\.CSharp\.Syntax\.[AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md#_Top)

