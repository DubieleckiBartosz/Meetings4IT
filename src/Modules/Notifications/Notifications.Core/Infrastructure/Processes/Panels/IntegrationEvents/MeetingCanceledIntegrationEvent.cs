using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;

//When the identifier is null, there is no alert because the user doesn't belong to the system
public record RecipientCanceledInvitation(string Email, string? Identifier = null);

[IntegrationEventDecorator(Navigators.MeetingCanceledNavigator)]
public record MeetingCanceledIntegrationEvent(string MeetingLink, string MeetingOrganizer, List<RecipientCanceledInvitation> Recipients) : IntegrationEvent;