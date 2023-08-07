namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class InvalidEmailException : BusinessException
{
    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
    }
}