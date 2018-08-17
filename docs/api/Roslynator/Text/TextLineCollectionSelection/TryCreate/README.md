# TextLineCollectionSelection\.TryCreate\(TextLineCollection, TextSpan, TextLineCollectionSelection\) Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.Text](../../README.md#_Top)\.[TextLineCollectionSelection](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Creates a new [TextLineCollectionSelection](../README.md#_Top) based on the specified list and span\.

```csharp
public static bool TryCreate(TextLineCollection lines, TextSpan span, out TextLineCollectionSelection selectedLines)
```

### Parameters

#### lines

#### span

#### selectedLines

### Returns

System\.[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the specified span contains at least one line; otherwise, false\.