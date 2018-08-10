# SyntaxInfo\.BinaryExpressionInfo Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [BinaryExpressionInfo(BinaryExpressionSyntax, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_) | Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md) from the specified binary expression\. |
| [BinaryExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md) from the specified node\. |

## BinaryExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md) from the specified binary expression\.

```csharp
public static BinaryExpressionInfo BinaryExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| binaryExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md)

## BinaryExpressionInfo\(SyntaxNode, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_BinaryExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md) from the specified node\.

```csharp
public static BinaryExpressionInfo BinaryExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[BinaryExpressionInfo](../../Syntax/BinaryExpressionInfo/README.md)
