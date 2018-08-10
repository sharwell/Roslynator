# SyntaxExtensions\.PreviousStatement\(StatementSyntax\) Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Gets the previous statement of the specified statement\.
If the specified statement is not contained in the list, or if there is no previous statement, then this method returns null\.

```csharp
public static StatementSyntax PreviousStatement(this StatementSyntax statement)
```

### Parameters

| Name | Summary |
| ---- | ------- |
| statement | |

### Returns

[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)
