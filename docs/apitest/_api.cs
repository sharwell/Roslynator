namespace Roslynator.Documentation.Test
{
  public class Foo
  {
    public readonly string Field;
    public Foo(string s);
    public Foo(string s, string s2);
    public Foo this[int index, int index2] { get; set; }
    public Foo this[int index] { get; set; }
    public void Bar();
    public void Bar2();
    public void Bar<T>(string value, string value2);
  }

  public class FooCollection
  {
    public FooCollection();
    public bool IsReadOnly { get; }
    public bool IsSynchronized { get; }
    public int Count { get; }
    public object SyncRoot { get; }
    public IEnumerator GetEnumerator();
    public bool Contains(Foo item);
    public bool Remove(Foo item);
    public void Add(Foo item);
    public void Clear();
    public void CopyTo(Array array, int index);
    public void CopyTo(Foo[] array, int arrayIndex);
  }

  public class FooDic<TKey, TValue> where TKey : Foo where TValue : Foo
  {
    public FooDic();
  }

  public class FooEvent
  {
    public FooEvent();
  }

  public sealed class MyAttribute
  {
    public MyAttribute();
  }

  public static class FooExtensions
  {
  }

  public interface IFoo
  {
    event EventHandler Changed;
  }

  public delegate void FooDelegate(object p)
}

public class Bla
{
  public Bla();
  public void M();
}
