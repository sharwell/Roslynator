# SyntaxExtensions\.GetRegionDirective\(EndRegionDirectiveTriviaSyntax\) Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Returns region directive that is related to the specified endregion directive\. Returns null if no matching region directive is found\.

```csharp
public static RegionDirectiveTriviaSyntax GetRegionDirective(this EndRegionDirectiveTriviaSyntax endRegionDirective)
```

### Parameters

#### endRegionDirective

### Returns

Microsoft\.CodeAnalysis\.CSharp\.Syntax\.[RegionDirectiveTriviaSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.regiondirectivetriviasyntax)

