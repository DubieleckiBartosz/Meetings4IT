using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.ScheduledMeetings.Events;

public record UpcomingMeetingRevoked(int MeetingRevoked, string ScheduleOwnerEmail) : IDomainEvent
{
    public static UpcomingMeetingRevoked Create(int meetingRevoked, Email scheduleOwnerEmail)
    {
        return new UpcomingMeetingRevoked(meetingRevoked, scheduleOwnerEmail);
    }
}