# SyntaxInfo\.NullCheckExpressionInfo Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [NullCheckExpressionInfo(SyntaxNode, NullCheckStyles, Boolean, Boolean)](#Roslynator_CSharp_SyntaxInfo_NullCheckExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_Roslynator_CSharp_NullCheckStyles_System_Boolean_System_Boolean_) | Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\. |
| [NullCheckExpressionInfo(SyntaxNode, SemanticModel, NullCheckStyles, Boolean, Boolean, CancellationToken)](#Roslynator_CSharp_SyntaxInfo_NullCheckExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_SemanticModel_Roslynator_CSharp_NullCheckStyles_System_Boolean_System_Boolean_System_Threading_CancellationToken_) | Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\. |

## NullCheckExpressionInfo\(SyntaxNode, NullCheckStyles, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_NullCheckExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_Roslynator_CSharp_NullCheckStyles_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\.

```csharp
public static NullCheckExpressionInfo NullCheckExpressionInfo(SyntaxNode node, NullCheckStyles allowedStyles = ComparisonToNull | IsPattern, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| allowedStyles | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md)

## NullCheckExpressionInfo\(SyntaxNode, SemanticModel, NullCheckStyles, Boolean, Boolean, CancellationToken\)<a name="Roslynator_CSharp_SyntaxInfo_NullCheckExpressionInfo_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_SemanticModel_Roslynator_CSharp_NullCheckStyles_System_Boolean_System_Boolean_System_Threading_CancellationToken_"></a>

### Summary

Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\.

```csharp
public static NullCheckExpressionInfo NullCheckExpressionInfo(SyntaxNode node, SemanticModel semanticModel, NullCheckStyles allowedStyles = All, bool walkDownParentheses = true, bool allowMissing = false, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| semanticModel | |
| allowedStyles | |
| walkDownParentheses | |
| allowMissing | |
| cancellationToken | |

#### Returns

[NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md)
