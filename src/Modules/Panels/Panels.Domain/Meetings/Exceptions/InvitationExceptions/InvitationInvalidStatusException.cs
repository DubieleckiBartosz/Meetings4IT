using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationInvalidStatusException : BusinessException
{
    public InvitationInvalidStatusException(string email, string currentStatus) : base(
        $"The invitation for {email} is currently {currentStatus} and cannot be canceled.")
    {
    }
}