using MediatR;
using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Notifications.Core.Constants;
using Notifications.Core.Domain.Alerts;
using Notifications.Core.Domain.Alerts.ValueTypes;
using Notifications.Core.Domain.Templates.ValueTypes;
using Notifications.Core.Handlers.Panels.Commands;
using Notifications.Core.Interfaces.Clients;
using Notifications.Core.Interfaces.Repositories;
using Notifications.Core.Models.Clients.EmailModels;
using Notifications.Core.Tools;
using Notifications.Core.Tools.Creators;
using Serilog;

namespace Notifications.Core.Handlers.Panels;

public class InvitationCreatedHandler : ICommandHandler<InvitationCreatedCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IEmailClient _emailClient;
    private readonly IAlertRepository _alertRepository;
    private readonly ITemplateRepository _templateRepository;

    public InvitationCreatedHandler(ILogger logger, IEmailClient emailClient, IAlertRepository alertRepository, ITemplateRepository templateRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
        _alertRepository = alertRepository ?? throw new ArgumentNullException(nameof(alertRepository));
        _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
    }

    public async Task<Unit> Handle(InvitationCreatedCommand request, CancellationToken cancellationToken)
    {
        var templateType = TemplateType.Invitation;
        var template = await _templateRepository.TemplateByTypeAsync(templateType, cancellationToken);
        if (template == null)
        {
            throw new NotFoundException($"Template {templateType} not found.");
        }

        var organizer = request.MeetingOrganizer;
        var meetingLink = request.MeetingLink;
        var invitationLink = request.InvitationLink;
        var recipient = request.Recipient;
        var recipientId = request.RecipientId;

        var dictData = TemplateCreator.TemplateInvitation(organizer, meetingLink, invitationLink);
        var emailMessageBody = template.Body.ReplaceData(dictData);
        var emailMessage = new EmailDetails(new List<string> { recipient }, Subjects.NewInvitation, emailMessageBody);

        _logger.Warning($"Sending mail with invitation to {recipient}...");

        await _emailClient.SendEmailAsync(emailMessage);

        _logger.Information($"Sent mail with invitation to {recipient}...");

        if (recipientId == null)
        {
            return Unit.Value;
        }

        var alertMessage = await _alertRepository.GetAlertDetailsByTypeAsync(AlertType.NewInvitation, cancellationToken);
        if (alertMessage == null)
        {
            throw new NotFoundException($"Alert {AlertType.NewInvitation} message not found.");
        }

        var dictAlertData = AlertMessageCreator.NewInvitationAlertMessage(organizer, meetingLink, invitationLink);

        var message = alertMessage.MessageTemplate.ReplaceData(dictAlertData);
        var newAlert = Alert.CreateAlert(AlertType.NewInvitation, message, recipientId);

        await _alertRepository.AddAlertAsync(newAlert, cancellationToken);

        _logger.Information($"New alert has been created: {newAlert.Id}");

        return Unit.Value;
    }
}