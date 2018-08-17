# BinaryExpressionInfo Struct <a name="_Top"></a>

[Home](../../../../README.md) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators)

**Namespace**: [Roslynator.CSharp.Syntax](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Provides information about binary expression\.

```csharp
public readonly struct BinaryExpressionInfo : System.IEquatable<BinaryExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; BinaryExpressionInfo

### Implements

* System\.[IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[BinaryExpressionInfo](#_Top)>

## Properties

| Property | Summary |
| -------- | ------- |
| [BinaryExpression](BinaryExpression/README.md#_Top) | The binary expression\. |
| [Kind](Kind/README.md#_Top) | The kind of the binary expression\. |
| [Left](Left/README.md#_Top) | The expression on the left of the binary operator\. |
| [Right](Right/README.md#_Top) | The expression on the right of the binary operator\. |
| [Success](Success/README.md#_Top) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [AsChain()](AsChain/README.md#_Top) | |
| [Equals(BinaryExpressionInfo)](Equals/README.md#Roslynator_CSharp_Syntax_BinaryExpressionInfo_Equals_Roslynator_CSharp_Syntax_BinaryExpressionInfo_) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<BinaryExpressionInfo>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [Equals(Object)](Equals/README.md#Roslynator_CSharp_Syntax_BinaryExpressionInfo_Equals_System_Object_) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [Expressions(Boolean)](Expressions/README.md#_Top) | \[deprecated\] Returns expressions of this binary expression, including expressions of nested binary expressions of the same kind as parent binary expression\. |
| [GetHashCode()](GetHashCode/README.md#_Top) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md#_Top) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(BinaryExpressionInfo, BinaryExpressionInfo)](op_Equality/README.md#_Top) | |
| [Inequality(BinaryExpressionInfo, BinaryExpressionInfo)](op_Inequality/README.md#_Top) | |

