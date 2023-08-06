using System.Text.RegularExpressions;
using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Meetings4IT.Shared.Domain.Kernel.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; set; }

    public Email(string value)
    {
        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        var match = regex.Match(value);
        if (!match.Success) 
        {
            throw new InvalidEmailException(value);
        }

        Value = value;
    }

    public static implicit operator Email?(string? value) => value == null ? null : new Email(value);

    public static implicit operator string(Email value) => value.Value; 
    public override string ToString() => Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}