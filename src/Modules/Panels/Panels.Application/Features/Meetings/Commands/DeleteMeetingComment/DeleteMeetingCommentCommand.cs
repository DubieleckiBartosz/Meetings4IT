using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters.MeetingParams;

namespace Panels.Application.Features.Meetings.Commands.DeleteMeetingComment;

public class DeleteMeetingCommentCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public int CommentId { get; }

    public DeleteMeetingCommentCommand(DeleteMeetingCommentParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        CommentId = parameters.CommentId;
    }
}