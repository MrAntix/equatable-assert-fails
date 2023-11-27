using System.Collections.Immutable;

namespace equatable_assert_fails;

public class NonEnumerableTests
{
    public sealed class It<T> :  IEquatable<It<T>>
    {
        public static readonly It<T> Empty = new();

        readonly ImmutableList<T> _items;
        It(IEnumerable<T>? items = null) => _items = items?.ToImmutableList() ?? [];

        public T this[int index] => ((IReadOnlyList<T>)_items)[index];

        public bool Equals(It<T>? other)
        {
            if (other is null) return false;
            if (Count != other.Count) return false;

            return _items.All(i => other._items.Count(o => Equals(o, i)) == 1);
        }
        public override bool Equals(object? obj) => Equals(obj as It<T>);
        public override int GetHashCode() => _items.Where(i => i != null)
                .Select(i => i!.GetHashCode())
                .Aggregate(0, (h, c) => h ^= c);

        public int Count => _items.Count;
        public It<T> AddRange(IEnumerable<T> items) => new(_items.AddRange(items));

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
