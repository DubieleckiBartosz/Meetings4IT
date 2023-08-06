namespace Meetings4IT.Shared.Domain.DomainExceptions;

public class InvalidEmailException : BusinessException
{ 
    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
    }
}