# SyntaxExtensions\.Add\(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean\) Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Creates a new list with the specified node added or inserted\.

```csharp
public static SyntaxList<StatementSyntax> Add(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions)
```

### Parameters

#### statements

#### statement

#### ignoreLocalFunctions

Insert statement before local function statements at the end of the list\.

### Returns

Microsoft\.CodeAnalysis\.[SyntaxList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\<Microsoft\.CodeAnalysis\.CSharp\.Syntax\.[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>

