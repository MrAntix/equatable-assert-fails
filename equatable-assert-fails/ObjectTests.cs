namespace equatable_assert_fails;

public class ObjectTests
{
    public sealed class It(string name) : IEquatable<It>
    {
        public string Name { get; } = name;

        public bool Equals(It? other) => Equals(Name, other?.Name);
        public override bool Equals(object? obj) => Equals(obj as It);
        public override int GetHashCode() => Name.GetHashCode();

        public static bool operator ==(It x, It y) => Equals(x, y);
        public static bool operator !=(It x, It y) => !Equals(x, y);
    }

    [Fact]
    public void Equal_Object()
    {
        const string name = "NAME";
        var x = new It(name);
        var y = new It(name);

        Assert.Equal(x, y);
    }
}
