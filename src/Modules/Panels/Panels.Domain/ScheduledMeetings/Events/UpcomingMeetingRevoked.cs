using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Notifications;

namespace Panels.Domain.ScheduledMeetings.Events;

public record UpcomingMeetingRevoked(int MeetingRevoked, string ScheduleOwnerEmail) : IDomainNotification
{
    public static UpcomingMeetingRevoked Create(int meetingRevoked, Email scheduleOwnerEmail)
    {
        return new UpcomingMeetingRevoked(meetingRevoked, scheduleOwnerEmail);
    }
}