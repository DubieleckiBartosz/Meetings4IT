using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.ScheduledMeetings.Events;

public record UpcomingMeetingRevoked(int MeetingRevoked, string ScheduleOwner) : IDomainEvent
{
    public static UpcomingMeetingRevoked Create(int meetingRevoked, string scheduleOwner)
    {
        return new UpcomingMeetingRevoked(meetingRevoked, scheduleOwner);
    }
}