# SyntaxExtensions\.IsKind Method

**Namespace**: [Roslynator.CSharp](../../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a trivia's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](#Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_) | Returns true if a token's kind is one of the specified kinds\. |

## IsKind\(SyntaxNode, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a node's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a node's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |
| kind3 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a node's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a node's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |
| kind5 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxNode_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a node's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |
| kind5 | |
| kind6 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxToken, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| kind1 | |
| kind2 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| kind1 | |
| kind2 | |
| kind3 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |
| kind5 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxToken_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxToken token, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |
| kind5 | |
| kind6 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxTrivia, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a trivia's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| trivia | |
| kind1 | |
| kind2 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| trivia | |
| kind1 | |
| kind2 | |
| kind3 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| trivia | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| trivia | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |
| kind5 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

## IsKind\(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind\)<a name="Roslynator_CSharp_SyntaxExtensions_IsKind_Microsoft_CodeAnalysis_SyntaxTrivia_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_Microsoft_CodeAnalysis_CSharp_SyntaxKind_"></a>

### Summary

Returns true if a token's kind is one of the specified kinds\.

```csharp
public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, SyntaxKind kind4, SyntaxKind kind5, SyntaxKind kind6)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| trivia | |
| kind1 | |
| kind2 | |
| kind3 | |
| kind4 | |
| kind5 | |
| kind6 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

