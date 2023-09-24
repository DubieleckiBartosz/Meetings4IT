using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Notifications.Core.Models.Views;

namespace Notifications.Core.Handlers.Alerts.Queries;

public record UnreadedAlertsQuery() : IQuery<Response<List<AlertViewModel>?>>;