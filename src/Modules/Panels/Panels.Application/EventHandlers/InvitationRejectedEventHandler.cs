using Meetings4IT.Shared.Implementations.Decorators;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Panels.Application.IntegrationEvents;
using Panels.Application.IntegrationEvents.Events;
using Panels.Application.Options;
using Panels.Domain.Meetings.Events;

namespace Panels.Application.EventHandlers;

public class InvitationRejectedEventHandler : IDomainEventHandler<InvitationRejected>
{
    private readonly PanelPathOptions _options;
    private readonly IPanelIntegrationEventService _panelIntegrationEventService;

    public InvitationRejectedEventHandler(
        IOptions<PanelPathOptions> options,
        IPanelIntegrationEventService panelIntegrationEventService)
    {
        _options = options.Value!;
        _panelIntegrationEventService = panelIntegrationEventService;
    }

    public async Task Handle(DomainEvent<InvitationRejected> notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var @event = notification.Event;
        var routeUriInvitation = new Uri(string.Concat($"{_options.ClientAddress}", _options.MeetingDetailsPath));
        var meetingLink = QueryHelpers.AddQueryString(routeUriInvitation.ToString(), "meetingId", @event.MeetingId);

        //Without logs
        await _panelIntegrationEventService.PublishThroughEventBusAsync(
            new InvitationRejectedIntegrationEvent(meetingLink, @event.OrganizerId, @event.RecipientName), cancellationToken);
    }
}