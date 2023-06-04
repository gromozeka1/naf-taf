using System.Collections;

namespace UI.PageObject.Elements;

public class ElementsCollection<T> : ICollection<T> where T : Element
{
    private readonly ICollection<T> _items;

    public ElementsCollection() => _items = new List<T>();

    public ElementsCollection(ICollection<T> items) => _items = items;

    public int Count => _items.Count;
    public bool IsReadOnly => _items.IsReadOnly;

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T item) => _items.Add(item);

    public void Clear() => _items.Clear();

    public bool Contains(T item) => _items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    public bool Remove(T item) => _items.Remove(item);

    public T this[int i] => _items.ToList()[i];
}
