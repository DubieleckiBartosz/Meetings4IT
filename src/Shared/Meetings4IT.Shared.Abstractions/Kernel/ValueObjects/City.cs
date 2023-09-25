using Meetings4IT.Shared.Abstractions.Exceptions;
using System.Text.RegularExpressions;

namespace Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

public class City : ValueObject
{
    public string Value { get; }

    public City(string value)
    {
        //only letters and spaces
        var regex = new Regex(@"^[A-Za-z\\s]*$");
        var match = regex.Match(value);
        if (!match.Success)
        {
            throw new InvalidEmailException(value);
        }

        Value = value;
    }

    public static implicit operator City(string value) => new City(value);

    public static implicit operator string(City value) => value.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}