using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationExpired(string RecipientInvitation, string MeetingOrganizer) : IDomainEvent
{
    public static InvitationExpired Create(string recipientInvitation, string meetingOrganizer)
    {
        return new InvitationExpired(recipientInvitation, meetingOrganizer);
    }
}