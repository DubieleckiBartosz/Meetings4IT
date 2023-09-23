using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters;

namespace Panels.Application.Features.Meetings.Commands.CreateMeetingInvitation;

public record CreateMeetingInvitationCommand : ICommand<Response<int>>
{
    public int MeetingId { get; }
    public DateTime InvitationExpirationDate { get; }
    public string? EmailInvitationRecipient { get; }
    public string NameInvitationRecipient { get; }
    public CreateMeetingInvitationCommand(CreateMeetingInvitationParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        InvitationExpirationDate = parameters.InvitationExpirationDate;
        EmailInvitationRecipient = parameters.EmailInvitationRecipient;
        NameInvitationRecipient = parameters.NameInvitationRecipient;
    }
}