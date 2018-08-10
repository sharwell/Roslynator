# IsExpressionInfo Struct

[Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators)

**Namespace**: [Roslynator.CSharp.Syntax](../README.md)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Provides information about "is" expression\.

```csharp
public readonly struct IsExpressionInfo : System.IEquatable<IsExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; IsExpressionInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[IsExpressionInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Expression](Expression/README.md) | The expression that is being casted\. |
| [IsExpression](IsExpression/README.md) | The "is" expression\. |
| [OperatorToken](OperatorToken/README.md) | The "is" operator token\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [Type](Type/README.md) | The type to which the expression is being cast\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(IsExpressionInfo)](Equals/README.md#Roslynator_CSharp_Syntax_IsExpressionInfo_Equals_Roslynator_CSharp_Syntax_IsExpressionInfo_) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<IsExpressionInfo>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [Equals(Object)](Equals/README.md#Roslynator_CSharp_Syntax_IsExpressionInfo_Equals_System_Object_) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(IsExpressionInfo, IsExpressionInfo)](op_Equality/README.md) | |
| [Inequality(IsExpressionInfo, IsExpressionInfo)](op_Inequality/README.md) | |
