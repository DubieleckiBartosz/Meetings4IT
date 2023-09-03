using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;
using Notifications.Core.Handlers.Identities.Commands;
using Notifications.Core.Interfaces.Clients;

namespace Notifications.Core.Handlers.Identities;

public class UserRegisteredHandler : ICommandHandler<UserRegisteredCommand, Unit>
{
    private readonly IEmailClient _emailClient;

    public UserRegisteredHandler(IEmailClient emailClient)
    {
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
    }

    public Task<Unit> Handle(UserRegisteredCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}