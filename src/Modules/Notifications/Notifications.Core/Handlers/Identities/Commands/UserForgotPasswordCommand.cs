using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Identities.Commands;

public class UserForgotPasswordCommand : ICommand<Unit>
{
    private string Email { get; }
    public string Link { get; }
    public string Token { get; }

    public UserForgotPasswordCommand(string email, string link, string token)
    {
        Email = email;
        Link = link;
        Token = token;
    }
}