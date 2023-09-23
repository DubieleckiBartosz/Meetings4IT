using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.ValueObjects;

public class NameInvitationRecipient : ValueObject
{
    public string Value { get; }

    public NameInvitationRecipient(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Value recipient name cannot be null or empty");
        }

        this.Value = value;
    }

    public static implicit operator NameInvitationRecipient(string value) => new(value);

    public static implicit operator string(NameInvitationRecipient recipient) => recipient.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}