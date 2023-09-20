using Meetings4IT.Shared.Implementations.Decorators;
using Panels.Application.Contracts.Repositories;
using Panels.Application.IntegrationEvents;
using Panels.Application.IntegrationEvents.Events;
using Panels.Domain.Meetings.Events;

namespace Panels.Application.EventHandlers;

public class InvitationCreatedEventHandler : IDomainEventHandler<NewInvitationCreated>
{
    private readonly IUserRepository _userRepository;
    private readonly IPanelIntegrationEventService _panelIntegrationEventService;

    public InvitationCreatedEventHandler(
        IUserRepository userRepository,
        IPanelIntegrationEventService panelIntegrationEventService)
    {
        _userRepository = userRepository;
        _panelIntegrationEventService = panelIntegrationEventService;
    }

    public async Task Handle(DomainEvent<NewInvitationCreated> notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var @event = notification.Event;
        var userResult = await _userRepository.GetUserByEmailAsync(@event.RecipientInvitation);

        var integrationEvent = new InvitationCreatedIntegrationEvent(
            @event.RecipientInvitation, userResult?.Identifier, @event.MeetingOrganizer, @event.MeetingId, @event.Code);

        await _panelIntegrationEventService.SaveEventAndPublishAsync(integrationEvent);
    }
}