using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Panels.Application.Features.Users.Commands.UpdateUserOpinion;

public record UpdateUserOpinionCommand(
    int UserId, int OpinionId, int? RatingSoftSkills = null, int? RatingTechSkills = null, string? Content = null) : ICommand<Response>;