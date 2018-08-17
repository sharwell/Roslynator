# SyntaxExtensions\.PreviousStatement\(StatementSyntax\) Method <a name="_Top"></a>

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Gets the previous statement of the specified statement\.
If the specified statement is not contained in the list, or if there is no previous statement, then this method returns null\.

```csharp
public static StatementSyntax PreviousStatement(this StatementSyntax statement)
```

### Parameters

#### statement

### Returns

Microsoft\.CodeAnalysis\.CSharp\.Syntax\.[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)

