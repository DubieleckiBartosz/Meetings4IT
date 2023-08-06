namespace Meetings4IT.Shared.Domain.DomainExceptions;

public class InvalidDateException : BusinessException
{
    public InvalidDateException(string message) : base(message)
    {
    }
}