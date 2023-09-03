using Meetings4IT.Shared.Abstractions.Kernel;
using Notifications.Core.Domain.Alerts.ValueTypes;

namespace Notifications.Core.Domain.Alerts.Entities;

public class Alert : Entity
{
    public AlertType Type { get; private set; }
    public bool IsRead { get; private set; }
    public string Description { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Modified { get; private set; }

    public Alert()
    {
    }

    Alert(AlertType type, bool isRead, string description, DateTime created, DateTime modified)
    {
        Type = type;
        IsRead = isRead;
        Description = description;
        Created = created;
        Modified = modified;
    }
}