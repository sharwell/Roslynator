# SemanticModelExtensions\.GetEnclosingSymbol\<TSymbol>\(SemanticModel, Int32, CancellationToken\) Method

**Containing Type**: [Roslynator](../../README.md)\.[SemanticModelExtensions](../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Returns the innermost symbol of type **TSymbol** that the specified position is considered inside of\.

```csharp
public static TSymbol GetEnclosingSymbol<TSymbol>(this SemanticModel semanticModel, int position, CancellationToken cancellationToken = default(CancellationToken)) where TSymbol : Microsoft.CodeAnalysis.ISymbol
```

### Type Parameters

| Name | Summary |
| ---- | ------- |
| TSymbol | |

### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| position | |
| cancellationToken | |

### Returns

TSymbol

