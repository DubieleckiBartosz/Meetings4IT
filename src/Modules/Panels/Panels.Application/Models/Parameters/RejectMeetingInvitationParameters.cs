using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class RejectMeetingInvitationParameters
{
    public int MeetingId { get; init; }
    public string InvitationCode { get; init; }

    public RejectMeetingInvitationParameters()
    { }

    [JsonConstructor]
    public RejectMeetingInvitationParameters(int meetingId, string invitationCode)
    {
        MeetingId = meetingId;
        InvitationCode = invitationCode;
    }
}