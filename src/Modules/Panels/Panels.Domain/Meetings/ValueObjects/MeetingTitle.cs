using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.ValueObjects;

public class MeetingTitle : ValueObject
{
    public string Value { get; }

    public MeetingTitle(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Title cannot be null or empty");
        }

        this.Value = value;
    }

    public static implicit operator MeetingTitle(string value) => new(value);

    public static implicit operator string(MeetingTitle meetingTitle) => meetingTitle.Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return this.Value;
    }
}