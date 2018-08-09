# CSharpExtensions\.GetTypeSymbol Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| GetTypeSymbol\(SemanticModel, AttributeSyntax, CancellationToken\) | Returns type information about an attribute syntax\. |
| GetTypeSymbol\(SemanticModel, ConstructorInitializerSyntax, CancellationToken\) | Returns type information about a constructor initializer syntax\. |
| GetTypeSymbol\(SemanticModel, ExpressionSyntax, CancellationToken\) | Returns type information about an expression syntax\. |
| GetTypeSymbol\(SemanticModel, SelectOrGroupClauseSyntax, CancellationToken\) | Returns type information about a select or group clause\. |

## GetTypeSymbol\(SemanticModel, AttributeSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetTypeSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns type information about an attribute syntax\.

```csharp
public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| attribute | |
| cancellationToken | |

#### Returns

[ITypeSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol)

## GetTypeSymbol\(SemanticModel, ConstructorInitializerSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetTypeSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns type information about a constructor initializer syntax\.

```csharp
public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| constructorInitializer | |
| cancellationToken | |

#### Returns

[ITypeSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol)

## GetTypeSymbol\(SemanticModel, ExpressionSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetTypeSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns type information about an expression syntax\.

```csharp
public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| expression | |
| cancellationToken | |

#### Returns

[ITypeSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol)

## GetTypeSymbol\(SemanticModel, SelectOrGroupClauseSyntax, CancellationToken\)<a name="Roslynator_CSharp_CSharpExtensions_GetTypeSymbol_Microsoft_CodeAnalysis_SemanticModel_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeSyntax_System_Threading_CancellationToken_"></a>

### Summary

Returns type information about a select or group clause\.

```csharp
public static ITypeSymbol GetTypeSymbol(this SemanticModel semanticModel, SelectOrGroupClauseSyntax selectOrGroupClause, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| selectOrGroupClause | |
| cancellationToken | |

#### Returns

[ITypeSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol)

