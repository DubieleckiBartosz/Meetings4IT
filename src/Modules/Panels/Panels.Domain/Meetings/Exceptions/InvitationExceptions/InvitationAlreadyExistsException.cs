using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationAlreadyExistsException : BaseException
{
    public InvitationAlreadyExistsException(string? resipient = null) : base($"Invitation already exists." + resipient != null ? $" [Recipient {resipient}]" : string.Empty)
    {
    }
}