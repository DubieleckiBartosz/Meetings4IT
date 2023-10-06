using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters.MeetingParams;

namespace Panels.Application.Features.Meetings.Commands.RejectInvitationRequest;

public class RejectInvitationRequestCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public int InvitationRequestId { get; }
    public string? Reason { get; }

    public RejectInvitationRequestCommand(RejectInvitationRequestParameters parameters)
    {
        this.MeetingId = parameters.MeetingId;
        this.InvitationRequestId = parameters.InvitationRequestId;
        this.Reason = parameters.Reason;
    }
}