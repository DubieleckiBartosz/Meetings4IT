using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MaxInvitationsReachedException : BusinessException
{
    public MaxInvitationsReachedException(int maxInvitations, int acceptedInvitations, int pendingInvitations) : base(
        $"The maximum number of invitations is {maxInvitations}. There are {pendingInvitations} invitations pending and {acceptedInvitations} have been accepted.")
    {
    }
}