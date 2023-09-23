namespace Notifications.Core.Tools;

public class AlertMessageCreator
{
    public static Dictionary<string, string> InvitationAlertMessage(string meetingOrganizer, string meetingLink, string invitationLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"MeetingOrganizer", meetingOrganizer},
            {"MeetingLink", meetingLink},
            {"InvitationLink", invitationLink}
        };

        return dictData;
    }
}