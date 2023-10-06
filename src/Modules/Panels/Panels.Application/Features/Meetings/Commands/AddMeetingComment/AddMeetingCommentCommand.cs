using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters.MeetingParams;

namespace Panels.Application.Features.Meetings.Commands.AddMeetingComment;

public class AddMeetingCommentCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public string Content { get; }

    public AddMeetingCommentCommand(AddMeetingCommentParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        Content = parameters.Content;
    }
}