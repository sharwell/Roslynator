# SyntaxExtensions\.IsLast\(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean\) Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Returns true if the specified statement is a last statement in the list\.

```csharp
public static bool IsLast(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions)
```

### Parameters

#### statements

#### statement

#### ignoreLocalFunctions

Ignore local function statements at the end of the list\.

### Returns

System\.[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

