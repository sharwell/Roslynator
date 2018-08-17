# SyntaxTreeExtensions Class <a name="_Top"></a>

[Home](../../README.md) &#x2022; [Methods](#methods)

**Namespace**: [Roslynator](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

A set of extension methods for [SyntaxTree](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtree)\.

```csharp
public static class SyntaxTreeExtensions
```

## Methods

| Method | Summary |
| ------ | ------- |
| [GetEndLine(SyntaxTree, TextSpan, CancellationToken)](GetEndLine/README.md#_Top) | Returns zero\-based index of the end line of the specified span\. |
| [GetStartLine(SyntaxTree, TextSpan, CancellationToken)](GetStartLine/README.md#_Top) | Returns zero\-based index of the start line of the specified span\. |
| [IsMultiLineSpan(SyntaxTree, TextSpan, CancellationToken)](IsMultiLineSpan/README.md#_Top) | Returns true if the specified [TextSpan](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.text.textspan) spans over multiple lines\. |
| [IsSingleLineSpan(SyntaxTree, TextSpan, CancellationToken)](IsSingleLineSpan/README.md#_Top) | Returns true if the specified [TextSpan](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.text.textspan) does not span over multiple lines\. |

