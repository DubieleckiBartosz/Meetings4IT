using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationAccepted(string MeetingId, string OrganizerId, string RecipientName) : IDomainEvent
{
    public static InvitationAccepted Create(string meetingId, string organizerId, string recipientName)
    {
        return new InvitationAccepted(meetingId, organizerId, recipientName);
    }
}