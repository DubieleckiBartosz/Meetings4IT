namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class InvalidDateException : BaseException
{
    public InvalidDateException(string message) : base(message)
    {
    }
}