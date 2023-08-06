using Meetings4IT.Shared.Domain.DomainExceptions;
using Meetings4IT.Shared.Domain.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationNotExpiredException : BusinessException
{
    public InvitationNotExpiredException(Date date) : base($"Invitation is not expired yet. The invitation expires on {date.Value}")
    {
    }
}