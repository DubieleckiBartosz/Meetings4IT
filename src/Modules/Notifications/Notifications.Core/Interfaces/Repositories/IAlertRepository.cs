using Notifications.Core.Domain.Alerts.ValueTypes;
using Notifications.Core.Domain.Alerts;

namespace Notifications.Core.Interfaces.Repositories;

public interface IAlertRepository
{
    Task AddAlertAsync(Alert alert, CancellationToken cancellationToken);

    Task<AlertDetails?> GetAlertDetailsByTypeAsync(AlertType alertType, CancellationToken cancellationToken);
}