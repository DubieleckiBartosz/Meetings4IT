using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;
using Notifications.Core.Handlers.Identities.Commands;
using Notifications.Core.Interfaces.Clients;

namespace Notifications.Core.Handlers.Identities;

public class UserForgotPasswordHandler : ICommandHandler<UserForgotPasswordCommand, Unit>
{
    private readonly IEmailClient _emailClient;

    public UserForgotPasswordHandler(IEmailClient emailClient)
    {
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
    }

    public async Task<Unit> Handle(UserForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}