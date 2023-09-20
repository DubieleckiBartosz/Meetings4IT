using Notifications.Core.Domain.Alerts.ValueTypes;

namespace Notifications.Core.Domain.Alerts;

public class AlertDetails
{
    public AlertType AlertDetailsId { get; }
    public string Title { get; }
    public string MessageTemplate { get; }

    public AlertDetails(AlertType messageId, string title, string messageTemplate)
    {
        AlertDetailsId = messageId;
        Title = title;
        MessageTemplate = messageTemplate;
    }
}