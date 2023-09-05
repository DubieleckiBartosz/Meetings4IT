using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationExpiredException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public InvitationExpiredException(Date date) : base($"The invitation expired on {date.Value}.")
    {
    }
}