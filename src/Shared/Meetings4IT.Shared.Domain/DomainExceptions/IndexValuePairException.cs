using System.Net;

namespace Meetings4IT.Shared.Domain.DomainExceptions;

public class IndexValuePairException : BusinessException
{
    public IndexValuePairException(string? title, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(title, message, statusCode)
    {
    } 
}