using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationAlreadyExistsException : BaseException
{
    public InvitationAlreadyExistsException(string email) : base($"Invitation for {email} already exists.")
    {
    }
}