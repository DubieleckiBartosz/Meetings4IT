using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Http;
using Panels.Application.Models.Parameters;

namespace Panels.Application.Features.Users.Commands.CompleteUserDetails;

public class CompleteUserDetailsCommand : ICommand<Response>
{
    public List<string>? Technologies { get; }
    public IFormFile? Image { get; }

    public CompleteUserDetailsCommand(CompleteUserDetailsParameters parameters)
    {
        Technologies = parameters.Technologies;
        Image = parameters.Image;
    }
}