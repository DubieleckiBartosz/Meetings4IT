using System.Net;

namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class IndexValuePairException : BaseException
{
    public IndexValuePairException(string? title, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(title, message, statusCode)
    {
    }
}