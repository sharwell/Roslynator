# FooDic\<TKey, TValue> Class

[Home](../../../../README.md) &#x2022; [Constructors](#constructors) &#x2022; [Methods](#methods) &#x2022; [Explicit Interface Implementations](#explicit-interface-implementations)

**Namespace**: [Roslynator.Documentation.Test](../README.md)

**Assembly**: Roslynator\.Documentation\.TestProject\.dll

**WARNING: This API is now obsolete\.**


```csharp
[System.ObsoleteAttribute]
public class FooDic<TKey, TValue> : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>,
    System.Collections.Generic.IEnumerable<TValue>,
    System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>,
    System.Collections.Generic.IReadOnlyList<System.Collections.Generic.KeyValuePair<TKey, TValue>>
    where TKey : Foo 
    where TValue : Foo
```

### Type Parameters

#### TKey





#### TValue





### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; FooDic\<TKey, TValue>

### Attributes

* System\.[ObsoleteAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.obsoleteattribute)

### Implements

* System\.Collections\.Generic\.[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[KeyValuePair\<TKey, TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2)>
* System\.Collections\.Generic\.[IEnumerable\<TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* System\.Collections\.Generic\.[IReadOnlyCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[KeyValuePair\<TKey, TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2)>
* System\.Collections\.Generic\.[IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)\<[KeyValuePair\<TKey, TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2)>

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [FooDic()](-ctor/README.md) | |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |
| [IEnumerable\<KeyValuePair\<TKey, TValue>>.GetEnumerator()](System-Collections-Generic-IEnumerable-System-Collections-Generic-KeyValuePair-TKey-TValue---GetEnumerator/README.md) | |
| [IEnumerable\<TValue>.GetEnumerator()](System-Collections-Generic-IEnumerable-TValue--GetEnumerator/README.md) | |
| [IReadOnlyCollection\<KeyValuePair\<TKey, TValue>>.Count](System-Collections-Generic-IReadOnlyCollection-System-Collections-Generic-KeyValuePair-TKey-TValue---Count/README.md) | |
| [IReadOnlyList\<KeyValuePair\<TKey, TValue>>.Item\[Int32\]](System-Collections-Generic-IReadOnlyList-System-Collections-Generic-KeyValuePair-TKey-TValue---Item/README.md) | |

