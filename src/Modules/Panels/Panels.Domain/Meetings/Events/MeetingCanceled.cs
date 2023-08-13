using Meetings4IT.Shared.Abstractions.Events;

namespace Panels.Domain.Meetings.Events;

public record MeetingCanceled(int MeetingId, List<string> InvitationRecipients) : IDomainEvent
{
    public static MeetingCanceled Create(int meetingId, List<string> invitationRecipients)
    {
        return new MeetingCanceled(meetingId, invitationRecipients);
    }
}