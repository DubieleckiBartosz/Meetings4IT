using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Notifications.Core.Models.Parameters;

namespace Notifications.Core.Handlers.Alerts.Commands.ReadAlerts;

public class ReadAlertsCommand : ICommand<Response>
{
    public List<int> AlertIdentifiers { get; }

    public ReadAlertsCommand(ReadAlertsParameters parameters)
    {
        AlertIdentifiers = parameters.AlertIdentifiers;
    }
}