using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects; 

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationNotExpiredException : BusinessException
{
    public InvitationNotExpiredException(Date date) : base($"Invitation is not expired yet. The invitation expires on {date.Value}")
    {
    }
}