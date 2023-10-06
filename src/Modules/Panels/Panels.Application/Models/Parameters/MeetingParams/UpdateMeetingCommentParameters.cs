using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class UpdateMeetingCommentParameters
{
    public int MeetingId { get; init; }
    public int CommentId { get; init; }
    public string Content { get; init; }

    public UpdateMeetingCommentParameters()
    { }

    [JsonConstructor]
    public UpdateMeetingCommentParameters(int meetingId, int commentId, string content)
    {
        MeetingId = meetingId;
        CommentId = commentId;
        Content = content;
    }
}