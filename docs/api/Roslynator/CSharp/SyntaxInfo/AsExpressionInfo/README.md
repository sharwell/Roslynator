# SyntaxInfo\.AsExpressionInfo Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [AsExpressionInfo(BinaryExpressionSyntax, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_CSharp_Syntax_BinaryExpressionSyntax_System_Boolean_System_Boolean_) | Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified binary expression\. |
| [AsExpressionInfo(SyntaxNode, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_) | Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified node\. |

## AsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified binary expression\.

```csharp
public static AsExpressionInfo AsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| binaryExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md)

## AsExpressionInfo\(SyntaxNode, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_AsExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified node\.

```csharp
public static AsExpressionInfo AsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md)

