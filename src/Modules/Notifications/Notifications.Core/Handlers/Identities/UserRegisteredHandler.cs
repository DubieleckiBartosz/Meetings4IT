using MediatR;
using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Notifications.Core.Constants;
using Notifications.Core.Domain.Templates.ValueTypes;
using Notifications.Core.Handlers.Identities.Commands;
using Notifications.Core.Interfaces.Clients;
using Notifications.Core.Interfaces.Repositories;
using Notifications.Core.Models.Clients.EmailModels;
using Notifications.Core.Tools.Creators;
using Serilog;

namespace Notifications.Core.Handlers.Identities;

public class UserRegisteredHandler : ICommandHandler<UserRegisteredCommand, Unit>
{
    private readonly IEmailClient _emailClient;
    private readonly ITemplateRepository _templateRepository;
    private readonly ILogger _logger;

    public UserRegisteredHandler(ILogger logger, IEmailClient emailClient, ITemplateRepository templateRepository)
    {
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
        _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(UserRegisteredCommand request, CancellationToken cancellationToken)
    {
        var templateType = TemplateType.Registration;
        var template = await _templateRepository.TemplateByTypeAsync(templateType, cancellationToken);
        if (template == null)
        {
            throw new NotFoundException($"Template {templateType} not found.");
        }

        var dictData = TemplateCreator.TemplateRegisterAccount(request.UserName, request.VerificationUri);
        var emailMessageBody = template.Body.ReplaceData(dictData);
        var emailMessage = new EmailDetails(new List<string> { request.Email }, Subjects.WelcomeNewUser, emailMessageBody);

        _logger.Warning($"Sending registration user mail to {request.Email}...");

        await _emailClient.SendEmailAsync(emailMessage);

        _logger.Information($"Sent registration user mail to {request.Email}...");

        return Unit.Value;
    }
}