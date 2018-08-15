# WorkspaceSyntaxExtensions\.Parenthesize\(ExpressionSyntax, Boolean, Boolean\) Method

[Home](../../../../README.md)

**Containing Type**: [Roslynator.CSharp](../../README.md)\.[WorkspaceSyntaxExtensions](../README.md)

**Assembly**: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Creates parenthesized expression that is parenthesizing the specified expression\.

```csharp
public static ParenthesizedExpressionSyntax Parenthesize(this ExpressionSyntax expression, bool includeElasticTrivia = true, bool simplifiable = true)
```

### Parameters

**expression**



**includeElasticTrivia**



**simplifiable**



### Returns

Microsoft\.CodeAnalysis\.CSharp\.Syntax\.[ParenthesizedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.parenthesizedexpressionsyntax)

