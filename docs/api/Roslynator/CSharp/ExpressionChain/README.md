# ExpressionChain Struct <a name="_Top"></a>

[Home](../../../README.md) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators) &#x2022; [Explicit Interface Implementations](#explicit-interface-implementations) &#x2022; [Structs](#structs)

**Namespace**: [Roslynator.CSharp](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Enables to enumerate expressions of a binary expression and expressions of nested binary expressions of the same kind as parent binary expression\.

```csharp
public readonly struct ExpressionChain : System.IEquatable<ExpressionChain>,
    System.Collections.Generic.IEnumerable<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; ExpressionChain

### Implements

* System\.[IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[ExpressionChain](#_Top)>
* System\.Collections\.Generic\.[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax)>

## Properties

| Property | Summary |
| -------- | ------- |
| [BinaryExpression](BinaryExpression/README.md#_Top) | The binary expression\. |
| [Span](Span/README.md#_Top) | The span that represents selected expressions\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(ExpressionChain)](Equals/README.md#Roslynator_CSharp_ExpressionChain_Equals_Roslynator_CSharp_ExpressionChain_) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<ExpressionChain>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [Equals(Object)](Equals/README.md#Roslynator_CSharp_ExpressionChain_Equals_System_Object_) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [GetEnumerator()](GetEnumerator/README.md#_Top) | Gets the enumerator for the expressions\. |
| [GetHashCode()](GetHashCode/README.md#_Top) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Reverse()](Reverse/README.md#_Top) | Returns a chain which contains all expressions of [ExpressionChain](#_Top) in reversed order\. |
| [ToString()](ToString/README.md#_Top) | Returns the string representation of the expressions, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(ExpressionChain, ExpressionChain)](op_Equality/README.md#_Top) | |
| [Inequality(ExpressionChain, ExpressionChain)](op_Inequality/README.md#_Top) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md#_Top) | |
| [IEnumerable\<ExpressionSyntax>.GetEnumerator()](System-Collections-Generic-IEnumerable-Microsoft-CodeAnalysis-CSharp-Syntax-ExpressionSyntax--GetEnumerator/README.md#_Top) | |

## Structs

| Struct | Summary |
| ------ | ------- |
| [Enumerator](Enumerator/README.md#_Top) | |
| [Reversed](Reversed/README.md#_Top) | Enables to enumerate expressions of [ExpressionChain](#_Top) in a reversed order\. |

