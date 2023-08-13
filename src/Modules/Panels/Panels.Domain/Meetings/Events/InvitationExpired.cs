using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationExpired(string RecipientInvitation, string MeetingCreator) : IDomainEvent
{
    public static InvitationExpired Create(string recipientInvitation, string meetingCreator)
    {
        return new InvitationExpired(recipientInvitation, meetingCreator);
    }
}