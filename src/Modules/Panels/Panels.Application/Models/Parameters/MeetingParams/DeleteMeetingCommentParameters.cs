using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class DeleteMeetingCommentParameters
{
    public int MeetingId { get; init; }
    public int CommentId { get; init; }

    public DeleteMeetingCommentParameters()
    { }

    [JsonConstructor]
    public DeleteMeetingCommentParameters(int meetingId, int commentId)
    {
        MeetingId = meetingId;
        CommentId = commentId;
    }
}