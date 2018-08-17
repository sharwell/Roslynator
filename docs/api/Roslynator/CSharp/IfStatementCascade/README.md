<a name="_Top"></a>

# IfStatementCascade Struct

[Home](../../../README.md#_Top) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators) &#x2022; [Explicit Interface Implementations](#explicit-interface-implementations) &#x2022; [Structs](#structs)

**Namespace**: [Roslynator.CSharp](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Enables to enumerate if statement cascade\.

```csharp
public readonly struct IfStatementCascade : System.IEquatable<IfStatementCascade>,
    System.Collections.Generic.IEnumerable<IfStatementOrElseClause>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; IfStatementCascade

### Implements

* System\.[IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[IfStatementCascade](#_Top)>
* System\.Collections\.Generic\.[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[IfStatementOrElseClause](../IfStatementOrElseClause/README.md#_Top)>

## Properties

| Property | Summary |
| -------- | ------- |
| [IfStatement](IfStatement/README.md#_Top) | The if statement\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(IfStatementCascade)](Equals/README.md#Roslynator_CSharp_IfStatementCascade_Equals_Roslynator_CSharp_IfStatementCascade_) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<IfStatementCascade>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [Equals(Object)](Equals/README.md#Roslynator_CSharp_IfStatementCascade_Equals_System_Object_) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [GetEnumerator()](GetEnumerator/README.md#_Top) | Gets the enumerator for the if\-else cascade\. |
| [GetHashCode()](GetHashCode/README.md#_Top) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md#_Top) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(IfStatementCascade, IfStatementCascade)](op_Equality/README.md#_Top) | |
| [Inequality(IfStatementCascade, IfStatementCascade)](op_Inequality/README.md#_Top) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md#_Top) | |
| [IEnumerable\<IfStatementOrElseClause>.GetEnumerator()](System-Collections-Generic-IEnumerable-Roslynator-CSharp-IfStatementOrElseClause--GetEnumerator/README.md#_Top) | |

## Structs

| Struct | Summary |
| ------ | ------- |
| [Enumerator](Enumerator/README.md#_Top) | |

