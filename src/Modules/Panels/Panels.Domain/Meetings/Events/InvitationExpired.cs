using Meetings4IT.Shared.Domain.Abstractions;

namespace Panels.Domain.Meetings.Events;

public record InvitationExpired(string RecipientInvitation, string MeetingCreator) : IDomainNotification
{
    public static InvitationExpired Create(string recipientInvitation, string meetingCreator)
    {
        return new InvitationExpired(recipientInvitation, meetingCreator);
    }
}