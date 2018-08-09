# SymbolExtensions\.GetDefaultValueSyntax Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| GetDefaultValueSyntax\(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat\) | Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\. |
| GetDefaultValueSyntax\(ITypeSymbol, TypeSyntax\) | Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\. |

## GetDefaultValueSyntax\(ITypeSymbol, TypeSyntax\)<a name="Roslynator_CSharp_SymbolExtensions_GetDefaultValueSyntax_Microsoft_CodeAnalysis_ITypeSymbol_Microsoft_CodeAnalysis_CSharp_Syntax_TypeSyntax_"></a>

### Summary

Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\.

```csharp
public static ExpressionSyntax GetDefaultValueSyntax(this ITypeSymbol typeSymbol, TypeSyntax type)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| typeSymbol | |
| type | |

#### Returns

[ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax)

## GetDefaultValueSyntax\(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat\)<a name="Roslynator_CSharp_SymbolExtensions_GetDefaultValueSyntax_Microsoft_CodeAnalysis_ITypeSymbol_Microsoft_CodeAnalysis_CSharp_Syntax_TypeSyntax_"></a>

### Summary

Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\.

```csharp
public static ExpressionSyntax GetDefaultValueSyntax(this ITypeSymbol typeSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| typeSymbol | |
| semanticModel | |
| position | |
| format | |

#### Returns

[ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax)

