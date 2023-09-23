using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class CreateMeetingInvitationParameters
{
    public int MeetingId { get; init; }
    public DateTime InvitationExpirationDate { get; init; }
    public string? EmailInvitationRecipient { get; init; }
    public string NameInvitationRecipient { get; init; }

    public CreateMeetingInvitationParameters()
    {
    }

    [JsonConstructor]
    public CreateMeetingInvitationParameters(
        DateTime invitationExpirationDate,
        string? emailInvitationRecipient,
        int meetingId,
        string nameInvitationRecipient)
    {
        InvitationExpirationDate = invitationExpirationDate;
        EmailInvitationRecipient = emailInvitationRecipient;
        MeetingId = meetingId;
        NameInvitationRecipient = nameInvitationRecipient;
    }
}