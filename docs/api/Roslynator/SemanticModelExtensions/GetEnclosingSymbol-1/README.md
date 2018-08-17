# SemanticModelExtensions\.GetEnclosingSymbol\<TSymbol>\(SemanticModel, Int32, CancellationToken\) Method <a name="_Top"></a>

[Home](../../../README.md)

**Containing Type**: [Roslynator](../../README.md#_Top)\.[SemanticModelExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Returns the innermost symbol of type **TSymbol** that the specified position is considered inside of\.

```csharp
public static TSymbol GetEnclosingSymbol<TSymbol>(this SemanticModel semanticModel, int position, CancellationToken cancellationToken = default(CancellationToken)) where TSymbol : Microsoft.CodeAnalysis.ISymbol
```

### Type Parameters

#### TSymbol

### Parameters

#### semanticModel

#### position

#### cancellationToken

### Returns

TSymbol

