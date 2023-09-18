using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationAccepted(int MeetingId, string OrganizerId, string AcceptedBy) : IDomainEvent
{
    public static InvitationAccepted Create(int meetingId, string organizerId, string acceptedBy)
    {
        return new InvitationAccepted(meetingId, organizerId, acceptedBy);
    }
}