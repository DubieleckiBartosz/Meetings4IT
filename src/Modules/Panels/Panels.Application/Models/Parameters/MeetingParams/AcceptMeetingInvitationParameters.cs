using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class AcceptMeetingInvitationParameters
{
    public int MeetingId { get; init; }
    public string InvitationCode { get; init; }

    public AcceptMeetingInvitationParameters()
    { }

    [JsonConstructor]
    public AcceptMeetingInvitationParameters(int meetingId, string invitationCode)
    {
        MeetingId = meetingId;
        InvitationCode = invitationCode;
    }
}