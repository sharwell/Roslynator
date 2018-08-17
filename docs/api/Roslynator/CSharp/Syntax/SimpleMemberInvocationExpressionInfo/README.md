<a name="_Top"></a>

# SimpleMemberInvocationExpressionInfo Struct

[Home](../../../../README.md#_Top) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Operators](#operators)

**Namespace**: [Roslynator.CSharp.Syntax](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Provides information about invocation expression\.

```csharp
public readonly struct SimpleMemberInvocationExpressionInfo : System.IEquatable<SimpleMemberInvocationExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; SimpleMemberInvocationExpressionInfo

### Implements

* System\.[IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[SimpleMemberInvocationExpressionInfo](#_Top)>

## Properties

| Property | Summary |
| -------- | ------- |
| [ArgumentList](ArgumentList/README.md#_Top) | The argumet list\. |
| [Arguments](Arguments/README.md#_Top) | The list of the arguments\. |
| [Expression](Expression/README.md#_Top) | The expression that contains the member being invoked\. |
| [InvocationExpression](InvocationExpression/README.md#_Top) | The invocation expression\. |
| [MemberAccessExpression](MemberAccessExpression/README.md#_Top) | The member access expression\. |
| [Name](Name/README.md#_Top) | The name of the member being invoked\. |
| [NameText](NameText/README.md#_Top) | The name of the member being invoked\. |
| [OperatorToken](OperatorToken/README.md#_Top) | The operator in the member access expression\. |
| [Success](Success/README.md#_Top) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md#Roslynator_CSharp_Syntax_SimpleMemberInvocationExpressionInfo_Equals_System_Object_) | Determines whether this instance and a specified object are equal\. \(Overrides [ValueType.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals)\) |
| [Equals(SimpleMemberInvocationExpressionInfo)](Equals/README.md#Roslynator_CSharp_Syntax_SimpleMemberInvocationExpressionInfo_Equals_Roslynator_CSharp_Syntax_SimpleMemberInvocationExpressionInfo_) | Determines whether this instance is equal to another object of the same type\. \(Implements [IEquatable\<SimpleMemberInvocationExpressionInfo>.Equals](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1.equals)\) |
| [GetHashCode()](GetHashCode/README.md#_Top) | Returns the hash code for this instance\. \(Overrides [ValueType.GetHashCode](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.gethashcode)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md#_Top) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. \(Overrides [ValueType.ToString](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(SimpleMemberInvocationExpressionInfo, SimpleMemberInvocationExpressionInfo)](op_Equality/README.md#_Top) | |
| [Inequality(SimpleMemberInvocationExpressionInfo, SimpleMemberInvocationExpressionInfo)](op_Inequality/README.md#_Top) | |

