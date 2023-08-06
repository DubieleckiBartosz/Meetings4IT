using Meetings4IT.Shared.Domain.Abstractions;
using Meetings4IT.Shared.Domain.Kernel.ValueObjects;

namespace Panels.Domain.ScheduledMeetings.Events;

public record UpcomingMeetingRevoked(int MeetingRevoked, string ScheduleOwnerEmail) : IDomainNotification
{
    public static UpcomingMeetingRevoked Create(int meetingRevoked, Email scheduleOwnerEmail)
    {
        return new UpcomingMeetingRevoked(meetingRevoked, scheduleOwnerEmail);
    }
}