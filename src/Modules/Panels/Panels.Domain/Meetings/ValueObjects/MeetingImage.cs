using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.ValueObjects;

public class MeetingImage : ValueObject
{
    public int MeetingId { get; }
    public string Key { get; }

    private MeetingImage(int meetingId, string key)
    {
        MeetingId = meetingId;
        Key = key;
    }

    public static MeetingImage Create(int meetingId, string key)
    {
        return new MeetingImage(meetingId, key);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return MeetingId;
        yield return Key;
    }
}