namespace Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

public class Content : ValueObject
{
    public string Value { get; }

    public Content(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Value content cannot be null or empty");
        }

        this.Value = value;
    }

    public static implicit operator Content(string value) => new(value);

    public static implicit operator string(Content content) => content.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}