using Meetings4IT.Shared.Domain.DomainExceptions;
using Meetings4IT.Shared.Domain.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationExpiredException : BusinessException
{
    public InvitationExpiredException(Date date) : base($"The invitation expired on {date.Value}.")
    {
    }
}