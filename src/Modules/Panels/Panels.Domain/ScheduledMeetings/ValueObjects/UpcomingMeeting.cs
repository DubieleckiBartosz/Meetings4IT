using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.ScheduledMeetings.ValueObjects;

public class UpcomingMeeting : ValueObject
{
    public int MeetingId { get; }
    public DateRange MeetingDateRange { get; }

    private UpcomingMeeting(int meetingId, DateRange meetingDateRange)
    {
        MeetingId = meetingId;
        MeetingDateRange = meetingDateRange;
    }

    public static UpcomingMeeting Create(int meetingId, DateRange meetingDateRange)
    {
        return new UpcomingMeeting(meetingId, meetingDateRange);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return MeetingId;
    }
}