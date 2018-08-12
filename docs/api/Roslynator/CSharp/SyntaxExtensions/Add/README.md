# SyntaxExtensions\.Add\(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean\) Method

[Home](../../../../README.md)

**Type**: [Roslynator.CSharp](../../README.md)\.[SyntaxExtensions](../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Creates a new list with the specified node added or inserted\.

```csharp
public static SyntaxList<StatementSyntax> Add(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions)
```

### Parameters

| Name | Summary |
| ---- | ------- |
| statements | |
| statement | |
| ignoreLocalFunctions | Insert statement before local function statements at the end of the list\. |

### Returns

Microsoft\.CodeAnalysis\.[SyntaxList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\<Microsoft\.CodeAnalysis\.CSharp\.Syntax\.[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>

