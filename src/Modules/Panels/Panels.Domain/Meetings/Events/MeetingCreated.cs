using Meetings4IT.Shared.Abstractions.Notifications; 

namespace Panels.Domain.Meetings.Events;

public record MeetingCreated(string MeetingCreator) : IDomainNotification
{
    public static MeetingCreated Create(string meetingCreator)
    {
        return new MeetingCreated(meetingCreator);
    }
}