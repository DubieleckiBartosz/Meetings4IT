using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class AddMeetingCommentParameters
{
    public int MeetingId { get; init; }
    public string Content { get; init; }

    public AddMeetingCommentParameters()
    { }

    [JsonConstructor]
    public AddMeetingCommentParameters(int meetingId, string content)
    {
        MeetingId = meetingId;
        Content = content;
    }
}