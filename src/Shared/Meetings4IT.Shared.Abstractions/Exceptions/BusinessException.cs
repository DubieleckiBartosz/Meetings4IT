using System.Net;

namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class BusinessException : Exception
{
    public string? Title { get; }
    public HttpStatusCode? StatusCode { get; }

    public BusinessException(string? title, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
        base(message)
    {
        Title = title;
        StatusCode = statusCode;
    }

    public BusinessException(string? title, string message, HttpStatusCode? statusCode = null) :
        base(message)
    {
        Title = title;
        StatusCode = statusCode;
    }

    public BusinessException(string message, HttpStatusCode statusCode) :
        this(null, message, statusCode)
    {
        StatusCode = statusCode;
    }

    public BusinessException(string message) :
        this(null, message, null)
    {
    }
}