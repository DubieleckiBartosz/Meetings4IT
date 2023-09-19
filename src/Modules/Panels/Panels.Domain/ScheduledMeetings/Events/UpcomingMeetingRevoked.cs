using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.ScheduledMeetings.Events;

public record UpcomingMeetingRevoked(Guid MeetingRevoked, string ScheduleOwner) : IDomainEvent
{
    public static UpcomingMeetingRevoked Create(Guid meetingRevoked, string scheduleOwner)
    {
        return new UpcomingMeetingRevoked(meetingRevoked, scheduleOwner);
    }
}