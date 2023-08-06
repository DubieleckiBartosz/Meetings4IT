using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationAlreadyExistsException : BusinessException
{
    public InvitationAlreadyExistsException(string email) : base($"Invitation for {email} already exists.")
    {
    }
}