using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Panels.Application.Features.Users.Commands.CreateNewUser;

public record CreateNewUserCommand(string Email, string Name, string UserId) : ICommand<Response>;