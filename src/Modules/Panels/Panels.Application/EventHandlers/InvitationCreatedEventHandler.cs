using Meetings4IT.Shared.Implementations.Decorators;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Panels.Application.Contracts.Repositories;
using Panels.Application.IntegrationEvents;
using Panels.Application.IntegrationEvents.Events;
using Panels.Application.Options;
using Panels.Domain.Meetings.Events;

namespace Panels.Application.EventHandlers;

public class InvitationCreatedEventHandler : IDomainEventHandler<NewInvitationCreated>
{
    private readonly PanelPathOptions _options;
    private readonly IUserRepository _userRepository;
    private readonly IPanelIntegrationEventService _panelIntegrationEventService;

    public InvitationCreatedEventHandler(
        IOptions<PanelPathOptions> options,
        IUserRepository userRepository,
        IPanelIntegrationEventService panelIntegrationEventService)
    {
        _options = options.Value!;
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
        var userResult = await _userRepository.GetUserByEmailNTAsync(@event.RecipientInvitation);

        var routeUriInvitation = new Uri(string.Concat($"{_options.ClientAddress}", _options.InvitationPath));

        var invitationLink = QueryHelpers.AddQueryString(routeUriInvitation.ToString(), "code", @event.Code);
        var meetingLink = QueryHelpers.AddQueryString(routeUriInvitation.ToString(), "meetingId", @event.MeetingId);

        var integrationEvent = new InvitationCreatedIntegrationEvent(
            @event.RecipientInvitation, userResult?.Identifier, @event.MeetingOrganizer, meetingLink, invitationLink);

        await _panelIntegrationEventService.SaveEventAndPublishAsync(integrationEvent);
    }
}