using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class DeleteInvitationRequestParameters
{
    public int MeetingId { get; init; }

    public DeleteInvitationRequestParameters()
    { }

    [JsonConstructor]
    public DeleteInvitationRequestParameters(int meetingId) => MeetingId = meetingId;
}