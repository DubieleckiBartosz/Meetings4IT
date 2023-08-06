using Meetings4IT.Shared.Domain.Abstractions;

namespace Panels.Domain.Meetings.Events;

public record MeetingCanceled(int MeetingId, List<string> InvitationRecipients) : IDomainNotification
{
    public static MeetingCanceled Create(int meetingId, List<string> invitationRecipients)
    {
        return new MeetingCanceled(meetingId, invitationRecipients);
    }
}