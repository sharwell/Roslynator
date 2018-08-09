# SyntaxExtensions\.AddAttributeLists Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [AddAttributeLists(ClassDeclarationSyntax, Boolean, AttributeListSyntax\[\])](#Roslynator_CSharp_SyntaxExtensions_AddAttributeLists_Microsoft_CodeAnalysis_CSharp_Syntax_ClassDeclarationSyntax_System_Boolean_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeListSyntax___) | Creates a new class declaration with the specified attribute lists added\. |
| [AddAttributeLists(InterfaceDeclarationSyntax, Boolean, AttributeListSyntax\[\])](#Roslynator_CSharp_SyntaxExtensions_AddAttributeLists_Microsoft_CodeAnalysis_CSharp_Syntax_InterfaceDeclarationSyntax_System_Boolean_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeListSyntax___) | Creates a new interface declaration with the specified attribute lists added\. |
| [AddAttributeLists(StructDeclarationSyntax, Boolean, AttributeListSyntax\[\])](#Roslynator_CSharp_SyntaxExtensions_AddAttributeLists_Microsoft_CodeAnalysis_CSharp_Syntax_StructDeclarationSyntax_System_Boolean_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeListSyntax___) | Creates a new struct declaration with the specified attribute lists added\. |

## AddAttributeLists\(ClassDeclarationSyntax, Boolean, AttributeListSyntax\[\]\)<a name="Roslynator_CSharp_SyntaxExtensions_AddAttributeLists_Microsoft_CodeAnalysis_CSharp_Syntax_ClassDeclarationSyntax_System_Boolean_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeListSyntax___"></a>

### Summary

Creates a new class declaration with the specified attribute lists added\.

```csharp
public static ClassDeclarationSyntax AddAttributeLists(this ClassDeclarationSyntax classDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| classDeclaration | |
| keepDocumentationCommentOnTop | If the declaration has no attribute lists and has a documentation comment the specified attribute lists will be inserted after the documentation comment\. |
| attributeLists | |

#### Returns

[ClassDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.classdeclarationsyntax)

## AddAttributeLists\(InterfaceDeclarationSyntax, Boolean, AttributeListSyntax\[\]\)<a name="Roslynator_CSharp_SyntaxExtensions_AddAttributeLists_Microsoft_CodeAnalysis_CSharp_Syntax_InterfaceDeclarationSyntax_System_Boolean_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeListSyntax___"></a>

### Summary

Creates a new interface declaration with the specified attribute lists added\.

```csharp
public static InterfaceDeclarationSyntax AddAttributeLists(this InterfaceDeclarationSyntax interfaceDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| interfaceDeclaration | |
| keepDocumentationCommentOnTop | If the declaration has no attribute lists and has a documentation comment the specified attribute lists will be inserted after the documentation comment\. |
| attributeLists | |

#### Returns

[InterfaceDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.interfacedeclarationsyntax)

## AddAttributeLists\(StructDeclarationSyntax, Boolean, AttributeListSyntax\[\]\)<a name="Roslynator_CSharp_SyntaxExtensions_AddAttributeLists_Microsoft_CodeAnalysis_CSharp_Syntax_StructDeclarationSyntax_System_Boolean_Microsoft_CodeAnalysis_CSharp_Syntax_AttributeListSyntax___"></a>

### Summary

Creates a new struct declaration with the specified attribute lists added\.

```csharp
public static StructDeclarationSyntax AddAttributeLists(this StructDeclarationSyntax structDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| structDeclaration | |
| keepDocumentationCommentOnTop | If the declaration has no attribute lists and has a documentation comment the specified attribute lists will be inserted after the documentation comment\. |
| attributeLists | |

#### Returns

[StructDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.structdeclarationsyntax)

