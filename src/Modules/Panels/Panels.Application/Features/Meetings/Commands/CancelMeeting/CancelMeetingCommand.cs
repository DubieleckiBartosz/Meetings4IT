using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters;

namespace Panels.Application.Features.Meetings.Commands.CancelMeeting;

public class CancelMeetingCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public string? Reason { get; }

    public CancelMeetingCommand(CancelMeetingParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        Reason = parameters.Reason;
    }
}