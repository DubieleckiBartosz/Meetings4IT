using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record MeetingCreated(string MeetingCreator) : IDomainEvent
{
    public static MeetingCreated Create(string meetingCreator)
    {
        return new MeetingCreated(meetingCreator);
    }
}