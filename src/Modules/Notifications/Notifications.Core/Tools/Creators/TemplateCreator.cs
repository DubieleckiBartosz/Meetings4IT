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
    public static Dictionary<string, string> TemplateInvitation(string meetingOrganizer, string meetingLink, string invitationLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"MeetingOrganizer", meetingOrganizer},
            {"MeetingLink", meetingLink},
            {"InvitationLink", invitationLink}
        };

        return dictData;
    }

    public static Dictionary<string, string> TemplateMeetingCancelation(string meetingOrganizer, string meetingLink)
    {
        var dictData = new Dictionary<string, string>
        {
            {"MeetingOrganizer", meetingOrganizer},
            {"MeetingLink", meetingLink},
        };

        return dictData;
    }
}