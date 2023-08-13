namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class InvalidEmailException : BaseException
{
    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
    }
}