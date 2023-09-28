using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationInvalidStatusException : BaseException
{
    public InvitationInvalidStatusException(string email, string currentStatus) : base(
        $"The invitation for {email} is currently {currentStatus} and cannot be canceled.")
    {
    }
}