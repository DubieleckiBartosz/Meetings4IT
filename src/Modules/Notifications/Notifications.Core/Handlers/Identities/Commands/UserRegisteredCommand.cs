﻿using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Identities.Commands;

public class UserRegisteredCommand : ICommand<Unit>
{
    public string Email { get; }
    public string UserName { get; }
    public string VerificationUri { get; }

    public UserRegisteredCommand(string email, string verificationUri, string userName)
    {
        Email = email;
        VerificationUri = verificationUri;
        UserName = userName;
    }
}