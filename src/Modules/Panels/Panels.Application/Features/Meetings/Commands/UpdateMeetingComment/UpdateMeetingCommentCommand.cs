using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters.MeetingParams;

namespace Panels.Application.Features.Meetings.Commands.UpdateMeetingComment;

public class UpdateMeetingCommentCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public int CommentId { get; }
    public string Content { get; }

    public UpdateMeetingCommentCommand(UpdateMeetingCommentParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        CommentId = parameters.CommentId;
        Content = parameters.Content;
    }
}