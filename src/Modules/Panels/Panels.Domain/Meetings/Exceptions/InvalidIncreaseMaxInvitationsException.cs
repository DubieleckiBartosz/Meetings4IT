namespace Panels.Domain.Meetings.Exceptions;

public class InvalidIncreaseMaxInvitationsException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public InvalidIncreaseMaxInvitationsException() : base("The number of invitations can only be greater than before")
    {
    }
}