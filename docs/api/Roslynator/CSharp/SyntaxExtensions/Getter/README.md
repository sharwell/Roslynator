# SyntaxExtensions\.Getter Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Getter\(AccessorListSyntax\) | Returns a get accessor contained in the specified list\. |
| Getter\(IndexerDeclarationSyntax\) | Returns a get accessor that is contained in the specified indexer declaration\. |
| Getter\(PropertyDeclarationSyntax\) | Returns property get accessor, if any\. |

## Getter\(AccessorListSyntax\)<a name="Roslynator_CSharp_SyntaxExtensions_Getter_Microsoft_CodeAnalysis_CSharp_Syntax_AccessorListSyntax_"></a>

### Summary

Returns a get accessor contained in the specified list\.

```csharp
public static AccessorDeclarationSyntax Getter(this AccessorListSyntax accessorList)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| accessorList | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)

## Getter\(IndexerDeclarationSyntax\)<a name="Roslynator_CSharp_SyntaxExtensions_Getter_Microsoft_CodeAnalysis_CSharp_Syntax_AccessorListSyntax_"></a>

### Summary

Returns a get accessor that is contained in the specified indexer declaration\.

```csharp
public static AccessorDeclarationSyntax Getter(this IndexerDeclarationSyntax indexerDeclaration)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| indexerDeclaration | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)

## Getter\(PropertyDeclarationSyntax\)<a name="Roslynator_CSharp_SyntaxExtensions_Getter_Microsoft_CodeAnalysis_CSharp_Syntax_AccessorListSyntax_"></a>

### Summary

Returns property get accessor, if any\.

```csharp
public static AccessorDeclarationSyntax Getter(this PropertyDeclarationSyntax propertyDeclaration)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| propertyDeclaration | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)

