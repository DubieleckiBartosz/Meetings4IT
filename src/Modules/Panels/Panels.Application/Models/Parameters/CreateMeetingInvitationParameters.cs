using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class CreateMeetingInvitationParameters
{
    public int MeetingId { get; init; }
    public DateTime InvitationExpirationDate { get; init; }
    public string InvitationRecipient { get; init; }

    public CreateMeetingInvitationParameters()
    {
    }

    [JsonConstructor]
    public CreateMeetingInvitationParameters(
        DateTime invitationExpirationDate,
        string invitationRecipient,
        int meetingId)
    {
        InvitationExpirationDate = invitationExpirationDate;
        InvitationRecipient = invitationRecipient;
        MeetingId = meetingId;
    }
}