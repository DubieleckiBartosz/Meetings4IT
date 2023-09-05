namespace Notifications.Core.Tools;

public class TemplateCreator
{
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
}