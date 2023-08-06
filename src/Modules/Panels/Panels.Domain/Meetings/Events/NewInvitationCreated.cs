using Meetings4IT.Shared.Domain.Abstractions;
using Meetings4IT.Shared.Domain.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Events;

public record NewInvitationCreated(string RecipientInvitation, string MeetingCreator) : IDomainNotification
{
    public static NewInvitationCreated Create(Email recipientInvitation, Email meetingCreator)
    {
        return new NewInvitationCreated(recipientInvitation, meetingCreator);
    }
}