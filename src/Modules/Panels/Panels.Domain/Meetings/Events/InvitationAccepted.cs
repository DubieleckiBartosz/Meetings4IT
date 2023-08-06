using Meetings4IT.Shared.Domain.Abstractions;

namespace Panels.Domain.Meetings.Events;

public record InvitationAccepted(string MeetingCreator) : IDomainNotification
{
    public static InvitationAccepted Create(string meetingCreator)
    {
        return new InvitationAccepted(meetingCreator);
    }
}