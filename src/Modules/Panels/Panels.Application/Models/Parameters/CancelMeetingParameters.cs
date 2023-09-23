using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class CancelMeetingParameters
{
    public int MeetingId { get; init; }
    public string? Reason { get; init; }

    public CancelMeetingParameters()
    { }

    [JsonConstructor]
    public CancelMeetingParameters(int meetingId, string reason)
    {
        MeetingId = meetingId;
        Reason = reason;
    }
}