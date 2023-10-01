namespace Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

public class UserInfo : ValueObject
{
    public string Identifier { get; }
    public string Name { get; }

    public UserInfo(
        string identifier,
        string name)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            throw new ArgumentException($"Identifier cannot be null or empty.", nameof(identifier));
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"Name cannot be null or empty.", nameof(name));
        }

        Identifier = identifier;
        Name = name;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Identifier;
        yield return Name;
    }
}