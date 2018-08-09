# WorkspaceSyntaxExtensions\.WithFormatterAnnotation Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [WithFormatterAnnotation(SyntaxToken)](../WithFormatterAnnotation/README.md#Roslynator_CSharp_WorkspaceSyntaxExtensions_WithFormatterAnnotation_Microsoft_CodeAnalysis_SyntaxToken_) | Adds [Formatter.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) to the specified token, creating a new token of the same type with the [Formatter.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) on it\. |
| [WithFormatterAnnotation\<TNode>(TNode)](#Roslynator_CSharp_WorkspaceSyntaxExtensions_WithFormatterAnnotation__1___0_) | Creates a new node with the [Formatter.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) attached\. |

## WithFormatterAnnotation\(SyntaxToken\)<a name="Roslynator_CSharp_WorkspaceSyntaxExtensions_WithFormatterAnnotation__1___0_"></a>

### Summary

Adds [Formatter.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) to the specified token, creating a new token of the same type with the [Formatter.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) on it\.

```csharp
public static SyntaxToken WithFormatterAnnotation(this SyntaxToken token)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)

## WithFormatterAnnotation\<TNode>\(TNode\)<a name="Roslynator_CSharp_WorkspaceSyntaxExtensions_WithFormatterAnnotation__1___0_"></a>

### Summary

Creates a new node with the [Formatter.Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) attached\.

```csharp
public static TNode WithFormatterAnnotation<TNode>(this TNode node) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |

#### Returns

TNode

