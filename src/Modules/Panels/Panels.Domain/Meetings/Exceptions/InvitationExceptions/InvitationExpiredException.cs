using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects; 

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationExpiredException : BusinessException
{
    public InvitationExpiredException(Date date) : base($"The invitation expired on {date.Value}.")
    {
    }
}