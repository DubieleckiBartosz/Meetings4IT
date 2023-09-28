using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Panels.Application.Features.Users.Commands.DeleteUserOpinion;

public record DeleteUserOpinionCommand(int UserId, int OpinionId) : ICommand<Response>;