using Notifications.Core.Domain.Alerts.ValueTypes;

namespace Notifications.Core.Models.Views;

public class AlertViewModel
{
    public int AlertIdentifier { get; }
    public AlertType Type { get; }
    public string Message { get; }
    public DateTime Created { get; }

    public AlertViewModel(
        int alertIdentifier,
        AlertType type,
        string message,
        DateTime created)
    {
        AlertIdentifier = alertIdentifier;
        Type = type;
        Message = message;
        Created = created;
    }
}