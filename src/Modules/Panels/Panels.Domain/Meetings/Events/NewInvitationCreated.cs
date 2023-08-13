using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Events;

public record NewInvitationCreated(string RecipientInvitation, string MeetingCreator) : IDomainEvent
{
    public static NewInvitationCreated Create(Email recipientInvitation, Email meetingCreator)
    {
        return new NewInvitationCreated(recipientInvitation, meetingCreator);
    }
}