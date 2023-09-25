﻿using MediatR;
using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Notifications.Core.Domain.Alerts;
using Notifications.Core.Domain.Alerts.ValueTypes;
using Notifications.Core.Handlers.Panels.Commands;
using Notifications.Core.Interfaces.Repositories;
using Notifications.Core.Tools;
using Notifications.Core.Tools.Creators;
using Serilog;

namespace Notifications.Core.Handlers.Panels;

public class InvitationRejectedHandler : ICommandHandler<InvitationRejectedCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IAlertRepository _alertRepository;

    public InvitationRejectedHandler(ILogger logger, IAlertRepository alertRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _alertRepository = alertRepository ?? throw new ArgumentNullException(nameof(alertRepository));
    }

    public async Task<Unit> Handle(InvitationRejectedCommand request, CancellationToken cancellationToken)
    {
        var alertMessage = await _alertRepository.GetAlertDetailsByTypeAsync(AlertType.InvitationRejected, cancellationToken);
        if (alertMessage == null)
        {
            throw new NotFoundException($"Alert {AlertType.InvitationRejected} message not found.");
        }

        var dictAlertData = AlertMessageCreator.InvitationRejectedAlertMessage(request.RejectedBy, request.MeetingLink);

        var message = alertMessage.MessageTemplate.ReplaceData(dictAlertData);
        var newAlert = Alert.CreateAlert(AlertType.InvitationRejected, message, request.RecipientId);

        await _alertRepository.AddAlertAsync(newAlert, cancellationToken);

        _logger.Information($"New alert has been created: {newAlert.Id}");

        return Unit.Value;
    }
}