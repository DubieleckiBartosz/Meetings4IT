using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Events;

public record NewInvitationCreated(string RecipientInvitation, string MeetingOrganizer, MeetingId MeetingId) : IDomainEvent
{
    public static NewInvitationCreated Create(Email recipientInvitation, string meetingOrganizer, MeetingId meetingId)
    {
        return new NewInvitationCreated(recipientInvitation, meetingOrganizer, meetingId);
    }
}