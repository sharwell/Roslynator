# WorkspaceSyntaxExtensions\.Parenthesize\(ExpressionSyntax, Boolean, Boolean\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Creates parenthesized expression that is parenthesizing the specified expression\.

```csharp
public static ParenthesizedExpressionSyntax Parenthesize(this ExpressionSyntax expression, bool includeElasticTrivia = true, bool simplifiable = true)
```

### Parameters

| Name | Summary |
| ---- | ------- |
| expression | |
| includeElasticTrivia | If true, add elastic trivia\. |
| simplifiable | If true, attach [Simplifier.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) to the parenthesized expression\. |

### Returns

[ParenthesizedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.parenthesizedexpressionsyntax)
