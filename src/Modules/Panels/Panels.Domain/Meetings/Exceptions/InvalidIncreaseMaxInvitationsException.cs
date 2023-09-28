using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class InvalidIncreaseMaxInvitationsException : BaseException
{
    public InvalidIncreaseMaxInvitationsException() : base("The number of invitations can only be greater than before")
    {
    }
}