using System.Collections;
using System.Collections.Immutable;

namespace equatable_assert_fails;

public class ImmutableListTests
{
    public sealed class It<T> : IImmutableList<T>, IEquatable<It<T>>
    {
        public static readonly It<T> Empty = new();

        readonly ImmutableList<T> _items;
        It(IEnumerable<T>? items = null) => _items = items?.ToImmutableList() ?? [];

        public T this[int index] => ((IReadOnlyList<T>)_items)[index];

        public bool Equals(It<T>? other)
        {
            if (other is null) return false;
            if (Count != other.Count) return false;

            return _items.All(i => other.Count(o => Equals(o, i)) == 1);
        }
        public override bool Equals(object? obj) => Equals(obj as It<T>);
        public override int GetHashCode() => _items.Where(i => i != null)
                .Select(i => i!.GetHashCode())
                .Aggregate(0, (h, c) => h ^= c);

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

        public int Count => _items.Count;
        public It<T> Add(T value) => new(_items.Add(value));
        IImmutableList<T> IImmutableList<T>.Add(T value) => Add(value);
        public It<T> AddRange(IEnumerable<T> items) => new(_items.AddRange(items));
        IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items) => AddRange(items);
        public It<T> Clear() => new(_items.Clear());
        IImmutableList<T> IImmutableList<T>.Clear() => Clear();
        public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer = null) => _items.IndexOf(item, index, count, equalityComparer);
        public It<T> Insert(int index, T element) => new(_items.Insert(index, element));
        IImmutableList<T> IImmutableList<T>.Insert(int index, T element) => Insert(index, element);
        public It<T> InsertRange(int index, IEnumerable<T> items) => new(_items.InsertRange(index, items));
        IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items) => InsertRange(index, items);
        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer = null) => _items.LastIndexOf(item, index, count, equalityComparer);
        public It<T> Remove(T value, IEqualityComparer<T>? equalityComparer = null) => new(_items.Remove(value, equalityComparer));
        IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T>? equalityComparer) => Remove(value, equalityComparer);
        public It<T> RemoveAll(Predicate<T> match) => new(_items.RemoveAll(match));
        IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match) => RemoveAll(match);
        public It<T> RemoveAt(int index) => new(_items.RemoveAt(index));
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index) => RemoveAt(index);
        public It<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer = null) => new(_items.RemoveRange(items, equalityComparer));
        IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer) => RemoveRange(items, equalityComparer);
        public It<T> RemoveRange(int index, int count) => new(_items.RemoveRange(index, count));
        IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count) => RemoveRange(index, count);
        public It<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer = null) => new(_items.Replace(oldValue, newValue, equalityComparer));
        IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer) => Replace(oldValue, newValue, equalityComparer);
        public It<T> SetItem(int index, T value) => new(_items.SetItem(index, value));
        IImmutableList<T> IImmutableList<T>.SetItem(int index, T value) => SetItem(index, value);

        public static bool operator ==(It<T> x, It<T> y) => Equals(x, y);
        public static bool operator !=(It<T> x, It<T> y) => !Equals(x, y);
    }

    [Fact]
    public void Equal_Collection()
    {
        const string name1 = "NAME_1";
        const string name2 = "NAME_2";

        var x = It<string>.Empty.AddRange([name1, name2]);
        var y = It<string>.Empty.AddRange([name2, name1]);

        Assert.Equal(x, y);
    }

    [Fact]
    public void Equal_Collection_Operator()
    {
        const string name1 = "NAME_1";
        const string name2 = "NAME_2";

        var x = It<string>.Empty.AddRange([name1, name2]);
        var y = It<string>.Empty.AddRange([name2, name1]);

        Assert.True(x == y);
    }
}
