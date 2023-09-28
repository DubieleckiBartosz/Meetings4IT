using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Panels.Application.Features.Users.Commands.AddNewUserOpinion;

public record AddNewUserOpinionCommand(
    int UserId, int? RatingSoftSkills = null, int? RatingTechSkills = null, string? Content = null) : ICommand<Response>;