namespace Notifications.Core.Tools;

public static class TemplateTools
{
    public static string ReplaceTemplateData(this string template, Dictionary<string, string> dictData)
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