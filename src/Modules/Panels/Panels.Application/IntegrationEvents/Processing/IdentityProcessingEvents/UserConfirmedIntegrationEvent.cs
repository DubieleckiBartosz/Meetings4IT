using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Panels.Application.IntegrationEvents.Processing.IdentityProcessingEvents;

[IntegrationEventDecorator(navigator: "UserConfirmedIntegrationEvent")]
public record UserConfirmedIntegrationEvent(string Email, string Name, string UserId) : IntegrationEvent;