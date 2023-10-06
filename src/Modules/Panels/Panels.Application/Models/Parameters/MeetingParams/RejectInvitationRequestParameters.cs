using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class RejectInvitationRequestParameters
{
    public int MeetingId { get; init; }
    public int InvitationRequestId { get; init; }
    public string? Reason { get; init; }

    public RejectInvitationRequestParameters()
    { }

    [JsonConstructor]
    public RejectInvitationRequestParameters(int meetingId, int invitationRequestId, string? reason) =>
        (MeetingId, InvitationRequestId, Reason) = (meetingId, invitationRequestId, reason);
}