using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record InvitationRejected(string MeetingOrganizerId) : IDomainEvent
{
    public static InvitationRejected Create(string meetingOrganizerId)
    {
        return new InvitationRejected(meetingOrganizerId);
    }
}