namespace Notifications.Core.Tools.Creators;

public static class Exchanger
{
    public static string ReplaceData(this string template, Dictionary<string, string> dictData)
    {
        if (template == null)
        {
            throw new ArgumentNullException(nameof(template));
        }

        foreach (var item in dictData.Keys)
        {
            template = template.Replace("{" + item + "}", dictData[item]);
        }

        return template;
    }
}