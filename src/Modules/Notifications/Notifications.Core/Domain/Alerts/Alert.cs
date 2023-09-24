using Meetings4IT.Shared.Abstractions.Time;
using Notifications.Core.Domain.Alerts.ValueTypes;

namespace Notifications.Core.Domain.Alerts;

public class Alert
{
    public int Id { get; }
    public string RecipientId { get; }
    public AlertType Type { get; }
    public bool IsRead { get; private set; }
    public string Message { get; }
    public DateTime Created { get; }

    private Alert()
    {
    }

    private Alert(AlertType type, string message, string recipientId)
    {
        RecipientId = recipientId;
        Type = type;
        IsRead = false;
        Message = message;
        Created = Clock.CurrentDate();
    }

    public static Alert CreateAlert(AlertType type, string message, string recipientId) => new Alert(type, message, recipientId);

    public void Read()
    {
        IsRead = true;
    }
}