using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters.MeetingParams;

namespace Panels.Application.Features.Meetings.Commands.DeleteInvitationRequest;

public class DeleteInvitationRequestCommand : ICommand<Response>
{
    public int MeetingId { get; }

    public DeleteInvitationRequestCommand(DeleteInvitationRequestParameters parameters)
    {
        MeetingId = parameters.MeetingId;
    }
}