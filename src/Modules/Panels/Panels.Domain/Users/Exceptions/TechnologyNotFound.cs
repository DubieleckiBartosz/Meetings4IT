using Meetings4IT.Shared.Domain.DomainExceptions;
using System.Net;

namespace Panels.Domain.Users.Exceptions;

public class TechnologyNotFound : BusinessException
{ 
    public TechnologyNotFound(string technology) : base($"Technology '{technology}' not found.", HttpStatusCode.NotFound)
    {
    }
}