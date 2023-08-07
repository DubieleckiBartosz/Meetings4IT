using System.Net;
using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Users.Exceptions;

public class TechnologyNotFound : BusinessException
{ 
    public TechnologyNotFound(string technology) : base($"Technology '{technology}' not found.", HttpStatusCode.NotFound)
    {
    }
}