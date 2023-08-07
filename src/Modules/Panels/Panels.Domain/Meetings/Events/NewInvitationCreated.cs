using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Notifications; 

namespace Panels.Domain.Meetings.Events;

public record NewInvitationCreated(string RecipientInvitation, string MeetingCreator) : IDomainNotification
{
    public static NewInvitationCreated Create(Email recipientInvitation, Email meetingCreator)
    {
        return new NewInvitationCreated(recipientInvitation, meetingCreator);
    }
}