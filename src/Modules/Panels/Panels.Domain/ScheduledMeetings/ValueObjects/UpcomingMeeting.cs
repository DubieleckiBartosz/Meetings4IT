using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Meetings;

namespace Panels.Domain.ScheduledMeetings.ValueObjects;

public class UpcomingMeeting : ValueObject
{
    public MeetingId MeetingId { get; }
    public DateRange MeetingDateRange { get; }

    private UpcomingMeeting()
    { }

    private UpcomingMeeting(MeetingId meetingId, DateRange meetingDateRange)
    {
        MeetingId = meetingId;
        MeetingDateRange = meetingDateRange;
    }

    public static UpcomingMeeting Create(MeetingId meetingId, DateRange meetingDateRange)
    {
        return new UpcomingMeeting(meetingId, meetingDateRange);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return MeetingId;
    }
}