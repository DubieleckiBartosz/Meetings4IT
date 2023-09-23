using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationRejected(string MeetingId, string OrganizerId, string RecipientName) : IDomainEvent
{
    public static InvitationRejected Create(string meetingId, string organizerId, string recipientName)
    {
        return new InvitationRejected(meetingId, organizerId, recipientName);
    }
}