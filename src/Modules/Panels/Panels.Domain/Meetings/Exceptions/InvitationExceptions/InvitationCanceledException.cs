using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationCanceledException : BaseException
{
    public InvitationCanceledException(int invitationId) : base($"The {invitationId} invitation has been canceled and cannot be changed.")
    {
    }
}