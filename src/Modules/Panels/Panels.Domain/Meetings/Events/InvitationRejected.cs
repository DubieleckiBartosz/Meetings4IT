using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationRejected(string MeetingCreator) : IDomainEvent
{
    public static InvitationRejected Create(string meetingCreator)
    {
        return new InvitationRejected(meetingCreator);
    }
}