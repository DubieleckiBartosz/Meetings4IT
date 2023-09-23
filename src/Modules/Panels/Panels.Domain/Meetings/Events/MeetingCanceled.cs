using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;
public record MeetingCanceled(string MeetingId, string OrganizerName, List<string>? InvitationRecipients) : IDomainEvent
{
    public static MeetingCanceled Create(string meetingId, string organizerName, List<string>? invitationRecipients)
    {
        return new MeetingCanceled(meetingId, organizerName, invitationRecipients);
    }
}