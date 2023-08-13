using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationAccepted(string MeetingCreator) : IDomainEvent
{
    public static InvitationAccepted Create(string meetingCreator)
    {
        return new InvitationAccepted(meetingCreator);
    }
}