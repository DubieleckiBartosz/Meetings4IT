namespace Notifications.Core.Tools;

public class AlertMessageCreator
{
    public static Dictionary<string, string> InvitationAlertMessage(string meetingOrganizer, string meetingId, string code)
    {
        var dictData = new Dictionary<string, string>
        {
            {"MeetingOrganizer", meetingOrganizer},
            {"MeetingId", meetingId},
            {"Code", code}
        };

        return dictData;
    }
}