namespace Notifications.Core.Tools.Creators;

public class TemplateCreator
{
    //IDENTITY
    public static Dictionary<string, string> TemplateRegisterAccount(string userName, string code)
    {
        var dictData = new Dictionary<string, string>
        {
            {"UserName", userName},
            {"VerificationUri", code}
        };
        return dictData;
    }

    public static Dictionary<string, string> TemplateResetPassword(string resetToken, string path)
    {
        var dictData = new Dictionary<string, string>
        {
            {"ResetToken", resetToken},
            {"Path", path}
        };
        return dictData;
    }

    //PANELS
    public static Dictionary<string, string> TemplateInvitation(string meetingOrganizer, string meetingId, string code)
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