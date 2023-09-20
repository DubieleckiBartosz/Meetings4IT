using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.ValueObjects;

public class InvitationCode : ValueObject
{
    public string Value { get; }

    private InvitationCode(string value)
    {
        Value = value;
    }

    public static InvitationCode Create(string value) => new(value);

    public static implicit operator InvitationCode(string value) => new(value);

    public static implicit operator string(InvitationCode code) => code.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}