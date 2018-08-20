<a name="_top"></a>

# FooCollection Class

[Home](../../../../README.md#_top) &#x2022; [Constructors](#constructors) &#x2022; [Properties](#properties) &#x2022; [Methods](#methods) &#x2022; [Explicit Interface Implementations](#explicit-interface-implementations)

**Namespace**: [Roslynator.Documentation.Test](../README.md#_top)

**Assembly**: Roslynator\.Documentation\.TestProject\.dll

```csharp
public class FooCollection : System.Collections.ICollection,
    System.Collections.Generic.ICollection<Foo>,
    System.Collections.Generic.IEnumerable<Foo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; FooCollection

### Implements

* System\.Collections\.[ICollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection)
* System\.Collections\.Generic\.[ICollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)\<[Foo](../Foo/README.md#_top)>
* System\.Collections\.Generic\.[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[Foo](../Foo/README.md#_top)>

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [FooCollection()](-ctor/README.md#_top) | |

## Properties

| Property | Summary |
| -------- | ------- |
| **[Count](Count/README.md#_top)** |  \(Implements [ICollection.Count](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection.count), [ICollection\<Foo>.Count](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.count)\) |
| **[IsReadOnly](IsReadOnly/README.md#_top)** |  \(Implements [ICollection\<Foo>.IsReadOnly](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.isreadonly)\) |
| **[IsSynchronized](IsSynchronized/README.md#_top)** |  \(Implements [ICollection.IsSynchronized](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection.issynchronized)\) |
| **[SyncRoot](SyncRoot/README.md#_top)** |  \(Implements [ICollection.SyncRoot](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection.syncroot)\) |

## Methods

| Method | Summary |
| ------ | ------- |
| **[Add(Foo)](Add/README.md#_top)** |  \(Implements [ICollection\<Foo>.Add](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.add)\) |
| **[Clear()](Clear/README.md#_top)** |  \(Implements [ICollection\<Foo>.Clear](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.clear)\) |
| **[Contains(Foo)](Contains/README.md#_top)** |  \(Implements [ICollection\<Foo>.Contains](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.contains)\) |
| **[CopyTo(Array, Int32)](CopyTo/README.md#Roslynator_Documentation_Test_FooCollection_CopyTo_System_Array_System_Int32_)** |  \(Implements [ICollection.CopyTo](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection.copyto)\) |
| **[CopyTo(Foo\[\], Int32)](CopyTo/README.md#Roslynator_Documentation_Test_FooCollection_CopyTo_Roslynator_Documentation_Test_Foo___System_Int32_)** |  \(Implements [ICollection\<Foo>.CopyTo](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.copyto)\) |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| **[GetEnumerator()](GetEnumerator/README.md#_top)** |  \(Implements [IEnumerable.GetEnumerator](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable.getenumerator)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| **[Remove(Foo)](Remove/README.md#_top)** |  \(Implements [ICollection\<Foo>.Remove](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.remove)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable\<Foo>.GetEnumerator()](System-Collections-Generic-IEnumerable-Roslynator-Documentation-Test-Foo--GetEnumerator/README.md#_top) | |

