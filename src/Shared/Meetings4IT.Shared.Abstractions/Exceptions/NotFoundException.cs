using System.Net;

namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class NotFoundException : BusinessException
{
    public NotFoundException(string? title, string message) :
        base(title, message, HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}