using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class InvalidIncreaseMaxInvitationsException : BusinessException
{
    public InvalidIncreaseMaxInvitationsException() : base("The number of invitations can only be greater than before")
    {
    }
}