using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Meetings.ValueObjects;

namespace Panels.Domain.Meetings.Events;

public record NewInvitationCreated(string RecipientInvitation, string MeetingOrganizer, string MeetingId, InvitationCode Code) : IDomainEvent
{
    public static NewInvitationCreated Create(Email recipientInvitation, string meetingOrganizer, MeetingId meetingId, InvitationCode code)
    {
        return new NewInvitationCreated(recipientInvitation, meetingOrganizer, meetingId.Value.ToString(), code);
    }
}