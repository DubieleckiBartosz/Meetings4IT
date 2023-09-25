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

public class MeetingCanceledHandler : ICommandHandler<MeetingCanceledCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IEmailClient _emailClient;
    private readonly IAlertRepository _alertRepository;
    private readonly ITemplateRepository _templateRepository;

    public MeetingCanceledHandler(
        ILogger logger,
        IEmailClient emailClient,
        IAlertRepository alertRepository,
        ITemplateRepository templateRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
        _alertRepository = alertRepository ?? throw new ArgumentNullException(nameof(alertRepository));
        _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
    }

    public async Task<Unit> Handle(MeetingCanceledCommand request, CancellationToken cancellationToken)
    {
        var templateType = TemplateType.MeetingCancelation;
        var template = await _templateRepository.TemplateByTypeAsync(templateType, cancellationToken);
        if (template == null)
        {
            throw new NotFoundException($"Template {templateType} not found.");
        }

        var organizer = request.MeetingOrganizer;
        var meetingLink = request.MeetingLink;
        var recipients = request.Recipients;

        var dictData = TemplateCreator.TemplateMeetingCancelation(organizer, meetingLink);
        var emailMessageBody = template.Body.ReplaceData(dictData);

        foreach (var recipientEmailMessage in recipients)
        {
            var recipientEmail = recipientEmailMessage.Email;
            var emailMessage = new EmailDetails(new List<string> { recipientEmail }, Subjects.MeetingCancelation, emailMessageBody);

            _logger.Warning($"Sending mail with information about meeting cancellation to {recipientEmail}...");

            await _emailClient.SendEmailAsync(emailMessage);

            _logger.Information($"Sent mail with information about meeting cancellation to {recipientEmail}...");
        }

        foreach (var recipientAlert in recipients)
        {
            if (recipientAlert.Identifier == null)
            {
                continue;
            }

            var alertMessage = await _alertRepository.GetAlertDetailsByTypeAsync(AlertType.MeetingCaneled, cancellationToken);
            if (alertMessage == null)
            {
                throw new NotFoundException($"Alert {AlertType.MeetingCaneled} message not found.");
            }

            var dictAlertData = AlertMessageCreator.MeetingCanceledAlertMessage(organizer, meetingLink);

            var message = alertMessage.MessageTemplate.ReplaceData(dictAlertData);
            var newAlert = Alert.CreateAlert(AlertType.MeetingCaneled, message, recipientAlert.Identifier);

            await _alertRepository.AddAlertAsync(newAlert, cancellationToken);

            _logger.Information($"New alert has been created: {newAlert.Id}");
        }

        return Unit.Value;
    }
}