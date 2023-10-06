using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters.MeetingParams;

namespace Panels.Application.Features.Meetings.Commands.AddInvitationRequest;

public class AddInvitationRequestCommand : ICommand<Response>
{
    public int MeetingId { get; }

    public AddInvitationRequestCommand(AddInvitationRequestParameters parameters) => (MeetingId) = (parameters.MeetingId);
}