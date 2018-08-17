<a name="_Top"></a>

# SyntaxExtensions\.AddUsings\(CompilationUnitSyntax, Boolean, UsingDirectiveSyntax\[\]\) Method

[Home](../../../../README.md#_Top)

**Containing Type**: [Roslynator.CSharp](../../README.md#_Top)\.[SyntaxExtensions](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Creates a new [CompilationUnitSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.compilationunitsyntax) with the specified using directives added\.

```csharp
public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit, bool keepSingleLineCommentsOnTop, params UsingDirectiveSyntax[] usings)
```

### Parameters

#### compilationUnit

#### keepSingleLineCommentsOnTop

#### usings

### Returns

Microsoft\.CodeAnalysis\.CSharp\.Syntax\.[CompilationUnitSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.compilationunitsyntax)

