# SimpleAssignmentStatementInfo Struct <a name="_Top"></a>

[Home](../../../../README.md) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators)

**Namespace**: [Roslynator.CSharp.Syntax](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Provides information about a simple assignment expression in an expression statement\.

```csharp
public readonly struct SimpleAssignmentStatementInfo : System.IEquatable<SimpleAssignmentStatementInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; SimpleAssignmentStatementInfo

### Implements

* System\.[IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[SimpleAssignmentStatementInfo](#_Top)>

## Properties

| Property | Summary |
| -------- | ------- |
| [AssignmentExpression](AssignmentExpression/README.md#_Top) | The simple assignment expression\. |
| [Left](Left/README.md#_Top) | The expression on the left of the assignment operator\. |
| [OperatorToken](OperatorToken/README.md#_Top) | The operator of the simple assignment expression\. |
| [Right](Right/README.md#_Top) | The expression of the right of the assignment operator\. |
| [Statement](Statement/README.md#_Top) | The expression statement the simple assignment expression is contained in\. |
| [Success](Success/README.md#_Top) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md#Roslynator_CSharp_Syntax_SimpleAssignmentStatementInfo_Equals_System_Object_) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [Equals(SimpleAssignmentStatementInfo)](Equals/README.md#Roslynator_CSharp_Syntax_SimpleAssignmentStatementInfo_Equals_Roslynator_CSharp_Syntax_SimpleAssignmentStatementInfo_) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<SimpleAssignmentStatementInfo>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [GetHashCode()](GetHashCode/README.md#_Top) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md#_Top) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(SimpleAssignmentStatementInfo, SimpleAssignmentStatementInfo)](op_Equality/README.md#_Top) | |
| [Inequality(SimpleAssignmentStatementInfo, SimpleAssignmentStatementInfo)](op_Inequality/README.md#_Top) | |

