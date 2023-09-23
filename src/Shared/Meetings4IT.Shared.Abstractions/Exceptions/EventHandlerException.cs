using System.Net;

namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class EventHandlerException : BaseException
{
    public EventHandlerException(string message, HttpStatusCode? code = null) : base(message, code ?? HttpStatusCode.InternalServerError)
    {
    }
}