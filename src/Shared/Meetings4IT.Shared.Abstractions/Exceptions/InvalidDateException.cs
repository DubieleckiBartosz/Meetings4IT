namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class InvalidDateException : BusinessException
{
    public InvalidDateException(string message) : base(message)
    {
    }
}