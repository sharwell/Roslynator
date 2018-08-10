# CSharpExtensions\.GetSymbol Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [GetSymbol(SemanticModel, AttributeSyntax, CancellationToken)](#Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeSyntax_System_Threading_CancellationToken_) | Returns what symbol, if any, the specified attribute syntax bound to\. |
| [GetSymbol(SemanticModel, ConstructorInitializerSyntax, CancellationToken)](#Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_ConstructorInitializerSyntax_System_Threading_CancellationToken_) | Returns what symbol, if any, the specified constructor initializer syntax bound to\. |
| [GetSymbol(SemanticModel, CrefSyntax, CancellationToken)](#Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_CrefSyntax_System_Threading_CancellationToken_) | Returns what symbol, if any, the specified cref syntax bound to\. |
| [GetSymbol(SemanticModel, ExpressionSyntax, CancellationToken)](#Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_System_Threading_CancellationToken_) | Returns what symbol, if any, the specified expression syntax bound to\. |
| [GetSymbol(SemanticModel, OrderingSyntax, CancellationToken)](#Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_OrderingSyntax_System_Threading_CancellationToken_) | Returns what symbol, if any, the specified ordering syntax bound to\. |
| [GetSymbol(SemanticModel, SelectOrGroupClauseSyntax, CancellationToken)](#Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_SelectOrGroupClauseSyntax_System_Threading_CancellationToken_) | Returns what symbol, if any, the specified select or group clause bound to\. |

## GetSymbol\(SemanticModel, AttributeSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns what symbol, if any, the specified attribute syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| attribute | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)

## GetSymbol\(SemanticModel, ConstructorInitializerSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_ConstructorInitializerSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns what symbol, if any, the specified constructor initializer syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| constructorInitializer | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)

## GetSymbol\(SemanticModel, CrefSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_CrefSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns what symbol, if any, the specified cref syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, CrefSyntax cref, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| cref | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)

## GetSymbol\(SemanticModel, ExpressionSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_ExpressionSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns what symbol, if any, the specified expression syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| expression | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)

## GetSymbol\(SemanticModel, OrderingSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_OrderingSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns what symbol, if any, the specified ordering syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, OrderingSyntax ordering, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| ordering | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)

## GetSymbol\(SemanticModel, SelectOrGroupClauseSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_SelectOrGroupClauseSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns what symbol, if any, the specified select or group clause bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, SelectOrGroupClauseSyntax selectOrGroupClause, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| selectOrGroupClause | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)
