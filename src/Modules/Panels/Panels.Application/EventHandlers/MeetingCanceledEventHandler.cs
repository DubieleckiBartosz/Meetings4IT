using Meetings4IT.Shared.Implementations.Decorators;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Panels.Application.Contracts.Repositories;
using Panels.Application.IntegrationEvents;
using Panels.Application.IntegrationEvents.Events;
using Panels.Application.Options;
using Panels.Domain.Meetings.Events;
using Serilog;

namespace Panels.Application.EventHandlers;

public class MeetingCanceledEventHandler : IDomainEventHandler<MeetingCanceled>
{
    private readonly PanelPathOptions _options;
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPanelIntegrationEventService _panelIntegrationEventService;

    public MeetingCanceledEventHandler(
        ILogger logger,
        IOptions<PanelPathOptions> options,
        IUserRepository userRepository,
        IPanelIntegrationEventService panelIntegrationEventService)
    {
        _options = options.Value!;
        _logger = logger;
        _userRepository = userRepository;
        _panelIntegrationEventService = panelIntegrationEventService;
    }

    public async Task Handle(DomainEvent<MeetingCanceled> notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var @event = notification.Event;
        if (@event.InvitationRecipients == null || !@event.InvitationRecipients.Any())
        {
            _logger.Warning($"No recipients of information about the meeting {@event.MeetingId} cancellation.");
            return;
        }

        var users = await _userRepository.GetUsersByEmailsNTAsync(@event.InvitationRecipients, cancellationToken);

        var recipients = new List<RecipientCanceledInvitation>();
        if (users != null && users.Any())
        {
            foreach (var InvitationRecipientItem in @event.InvitationRecipients)
            {
                var user = users.FirstOrDefault(_ => _.Email == InvitationRecipientItem);
                if (user != null)
                {
                    recipients.Add(new RecipientCanceledInvitation(InvitationRecipientItem, user.Identifier));
                    continue;
                }

                recipients.Add(new RecipientCanceledInvitation(InvitationRecipientItem));
            }
        }

        var routeUriInvitation = new Uri(string.Concat($"{_options.ClientAddress}", _options.MeetingPath));
        var meetingLink = QueryHelpers.AddQueryString(routeUriInvitation.ToString(), "meetingId", @event.MeetingId);

        //Without logs
        await _panelIntegrationEventService.PublishThroughEventBusAsync(
            new MeetingCanceledIntegrationEvent(meetingLink, @event.OrganizerName, recipients), cancellationToken);
    }
}