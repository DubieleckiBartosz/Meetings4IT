using Meetings4IT.Shared.Domain.Abstractions;

namespace Panels.Domain.Meetings.Events;

public record InvitationRejected(string MeetingCreator) : IDomainNotification
{
    public static InvitationRejected Create(string meetingCreator)
    {
        return new InvitationRejected(meetingCreator);
    }
}