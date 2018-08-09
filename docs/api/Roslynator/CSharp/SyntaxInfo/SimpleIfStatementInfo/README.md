# SyntaxInfo\.SimpleIfStatementInfo Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| SimpleIfStatementInfo\(IfStatementSyntax, Boolean, Boolean\) | Creates a new [SimpleIfStatementInfo](../../Syntax/SimpleIfStatementInfo/README.md) from the specified if statement\. |
| SimpleIfStatementInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [SimpleIfStatementInfo](../../Syntax/SimpleIfStatementInfo/README.md) from the specified node\. |

## SimpleIfStatementInfo\(SyntaxNode, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_SimpleIfStatementInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [SimpleIfStatementInfo](../../Syntax/SimpleIfStatementInfo/README.md) from the specified node\.

```csharp
public static SimpleIfStatementInfo SimpleIfStatementInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleIfStatementInfo](../../Syntax/SimpleIfStatementInfo/README.md)

## SimpleIfStatementInfo\(IfStatementSyntax, Boolean, Boolean\)<a name="Roslynator_CSharp_SyntaxInfo_SimpleIfStatementInfo_Microsoft_CodeAnalysis_SyntaxNode_System_Boolean_System_Boolean_"></a>

### Summary

Creates a new [SimpleIfStatementInfo](../../Syntax/SimpleIfStatementInfo/README.md) from the specified if statement\.

```csharp
public static SimpleIfStatementInfo SimpleIfStatementInfo(IfStatementSyntax ifStatement, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| ifStatement | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleIfStatementInfo](../../Syntax/SimpleIfStatementInfo/README.md)

