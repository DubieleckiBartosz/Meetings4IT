using System.Net;

namespace Panels.Domain.Users.Exceptions;

public class TechnologyNotFound : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public TechnologyNotFound(string technology) : base($"Technology '{technology}' not found.", HttpStatusCode.NotFound)
    {
    }
}