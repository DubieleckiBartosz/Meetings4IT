using Meetings4IT.Shared.Abstractions.Notifications; 

namespace Panels.Domain.Meetings.Events;

public record InvitationAccepted(string MeetingCreator) : IDomainNotification
{
    public static InvitationAccepted Create(string meetingCreator)
    {
        return new InvitationAccepted(meetingCreator);
    }
}