using Meetings4IT.Shared.Abstractions.Exceptions;
using System.Net;

namespace Panels.Domain.Users.Exceptions;

public class TechnologyNotFoundException : BaseException
{
    public TechnologyNotFoundException(string technology) : base($"Technology '{technology}' not found.", HttpStatusCode.NotFound)
    {
    }
}