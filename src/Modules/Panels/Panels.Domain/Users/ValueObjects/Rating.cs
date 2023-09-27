using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Users.ValueObjects;

public class Rating : ValueObject
{
    public int Value { get; }

    public Rating(int value)
    {
        if (value < 1 || value > 5)
        {
            throw new ArgumentOutOfRangeException("Rating value must be between 1 and 5");
        }

        this.Value = value;
    }

    public static implicit operator Rating?(int value) => new(value);

    public static implicit operator int(Rating rating) => rating.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}