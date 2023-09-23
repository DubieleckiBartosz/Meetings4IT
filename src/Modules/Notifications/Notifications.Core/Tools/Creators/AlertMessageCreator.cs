namespace Notifications.Core.Tools;

public class AlertMessageCreator
{
    public static Dictionary<string, string> NewInvitationAlertMessage(string meetingOrganizer, string meetingLink, string invitationLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"MeetingOrganizer", meetingOrganizer},
            {"MeetingLink", meetingLink},
            {"InvitationLink", invitationLink}
        };

        return dictData;
    }

    public static Dictionary<string, string> InvitationAcceptedAlertMessage(string acceptedBy, string meetingLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"NameInvitationRecipient", acceptedBy},
            {"MeetingLink", meetingLink}
        };

        return dictData;
    }

    public static Dictionary<string, string> InvitationRejectedAlertMessage(string rejectedBy, string meetingLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"RejectedBy", rejectedBy},
            {"MeetingLink", meetingLink}
        };

        return dictData;
    }

    public static Dictionary<string, string> MeetingCanceledAlertMessage(string organizer, string meetingLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"MeetingOrganizer", organizer},
            {"MeetingLink", meetingLink}
        };

        return dictData;
    }
}