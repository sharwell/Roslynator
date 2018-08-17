# SyntaxListSelection\<TNode> Class <a name="_Top"></a>

[Home](../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Explicit Interface Implementations](#explicit-interface-implementations) &#x2022; [Structs](#structs)

**Namespace**: [Roslynator](../README.md#_Top)

**Assembly**: Roslynator\.CSharp\.dll

## Summary

Represents selected nodes in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public class SyntaxListSelection<TNode> : ISelection<TNode>,
    System.Collections.Generic.IEnumerable<TNode>,
    System.Collections.Generic.IReadOnlyCollection<TNode>,
    System.Collections.Generic.IReadOnlyList<TNode>
    where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

### Type Parameters

#### TNode

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; SyntaxListSelection\<TNode>

### Derived

* Roslynator\.CSharp\.[MemberDeclarationListSelection](../CSharp/MemberDeclarationListSelection/README.md#_Top)
* Roslynator\.CSharp\.[StatementListSelection](../CSharp/StatementListSelection/README.md#_Top)

### Implements

* System\.Collections\.Generic\.[IEnumerable\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* System\.Collections\.Generic\.[IReadOnlyCollection\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* System\.Collections\.Generic\.[IReadOnlyList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)
* Roslynator\.[ISelection\<TNode>](../ISelection-1/README.md#_Top)

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [SyntaxListSelection(SyntaxList\<TNode>, TextSpan, Int32, Int32)](-ctor/README.md#_Top) | Initializes a new instance of the [SyntaxListSelection\<TNode>](#_Top)\. |

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](Count/README.md#_Top) | Gets a number of selected nodes\. \(Implements [IReadOnlyCollection\<TNode>.Count](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1.count)\) |
| [FirstIndex](FirstIndex/README.md#_Top) | Gets an index of the first selected node\. \(Implements [ISelection\<TNode>.FirstIndex](../ISelection-1/FirstIndex/README.md#_Top)\) |
| [Item\[Int32\]](Item/README.md#_Top) | Gets the selected node at the specified index\. \(Implements [IReadOnlyList\<TNode>.Item](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1.item)\) |
| [LastIndex](LastIndex/README.md#_Top) | Gets an index of the last selected node\. \(Implements [ISelection\<TNode>.LastIndex](../ISelection-1/LastIndex/README.md#_Top)\) |
| [OriginalSpan](OriginalSpan/README.md#_Top) | Gets the original span that was used to determine selected nodes\. |
| [UnderlyingList](UnderlyingList/README.md#_Top) | Gets an underlying list that contains selected nodes\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(SyntaxList\<TNode>, TextSpan)](Create/README.md#_Top) | Creates a new [SyntaxListSelection\<TNode>](#_Top) based on the specified list and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](First/README.md#_Top) | Gets the first selected node\. \(Implements [ISelection\<TNode>.First](../ISelection-1/First/README.md#_Top)\) |
| [GetEnumerator()](GetEnumerator/README.md#_Top) | Returns an enumerator that iterates through selected nodes\. |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](Last/README.md#_Top) | Gets the last selected node\. \(Implements [ISelection\<TNode>.Last](../ISelection-1/Last/README.md#_Top)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [TryCreate(SyntaxList\<TNode>, TextSpan, SyntaxListSelection\<TNode>)](TryCreate/README.md#_Top) | Creates a new [SyntaxListSelection\<TNode>](#_Top) based on the specified list and span\. |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md#_Top) | |
| [IEnumerable\<TNode>.GetEnumerator()](System-Collections-Generic-IEnumerable-TNode--GetEnumerator/README.md#_Top) | |

## Structs

| Struct | Summary |
| ------ | ------- |
| [Enumerator](Enumerator/README.md#_Top) | |

