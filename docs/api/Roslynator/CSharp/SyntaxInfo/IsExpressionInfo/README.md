# SyntaxInfo\.IsExpressionInfo Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\) | Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified binary expression\. |
| IsExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified node\. |

## IsExpressionInfo\(SyntaxNode, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_IsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified node\.

```csharp
public static IsExpressionInfo IsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md)

## IsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_IsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified binary expression\.

```csharp
public static IsExpressionInfo IsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| binaryExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md)

