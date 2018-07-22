using System;
using System.Collections;
using System.Collections.Generic;
using ;

namespace System
{
  public class ClassName
  {
    public ClassName();
  }
}

namespace Roslynator.Documentation.Test
{
  [Obsolete]
  public class Foo : Bla
  {
    public readonly string Field;

    public Foo(string s);
    public Foo(string s, string s2);

    public Foo this[int index, int index2] { get; set; }
    public Foo this[int index] { get; set; }

    public void Bar();
    public void Bar<T>(string value, string value2);
    public void Bar2();
  }

  public class FooCollection : ICollection<Foo>, IEnumerable<Foo>, ICollection
  {
    public FooCollection();

    public int Count { get; }
    public bool IsReadOnly { get; }
    public bool IsSynchronized { get; }
    public object SyncRoot { get; }

    public void Add(Foo item);
    public void Clear();
    public bool Contains(Foo item);
    public void CopyTo(Array array, int index);
    public void CopyTo(Foo[] array, int arrayIndex);
    public IEnumerator GetEnumerator();
    public bool Remove(Foo item);
  }

  [Obsolete]
  public class FooDic<TKey, TValue> : IReadOnlyList<KeyValuePair<TKey, TValue>>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable<TValue> where TKey : Foo where TValue : Foo
  {
    public FooDic();
  }

  public class FooEvent : IFoo
  {
    public FooEvent();
  }

  public static class FooExtensions
  {
  }

  [System.AttributeUsageAttribute]
  public sealed class MyAttribute : Attribute
  {
    public MyAttribute();
  }

  public interface IFoo
  {
    event EventHandler Changed;
  }

  public delegate void FooDelegate(object p)
}

[System.CLSCompliantAttribute]
public class Bla
{
  public Bla();

  public void M();
}
