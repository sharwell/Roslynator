# SyntaxExtensions\.ToSyntaxList\<TNode>\(IEnumerable\<TNode>\) Method

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md)\.[SyntaxExtensions](../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Creates a list of syntax nodes from a sequence of nodes\.

```csharp
public static SyntaxList<TNode> ToSyntaxList<TNode>(this IEnumerable<TNode> nodes) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

### Parameters

| Name | Summary |
| ---- | ------- |
| nodes | |

### Returns

Microsoft\.CodeAnalysis\.[SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)

