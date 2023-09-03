using Notifications.Core.Domain.Templates.ValueTypes;

namespace Notifications.Core.Domain.Templates;

public class Template
{
    public int Id { get; private set; }
    public string Body { get; private set; }
    public TemplateType Type { get; private set; }
    public Template()
    {
    }

    public Template(int id, string body, TemplateType type)
    {
        Id = id;
        Body = body;
        Type = type;
    }
}