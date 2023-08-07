using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects; 

namespace Panels.Domain.Meetings.ValueObjects;

public class Description : ValueObject
{
    public string Value { get; }

    public Description(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Title cannot be null or empty");
        }

        this.Value = value;
    }

    public static implicit operator Description(string value) => new(value);
    public static implicit operator string(Description description) => description.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }

}