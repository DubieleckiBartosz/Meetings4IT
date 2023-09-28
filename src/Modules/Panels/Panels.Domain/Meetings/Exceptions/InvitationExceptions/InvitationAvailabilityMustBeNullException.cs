using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationAvailabilityMustBeNullException : BaseException
{
    public InvitationAvailabilityMustBeNullException() : base(
        "If the meeting is public, we cannot set the availability of invitations because it can cause conflicts.")
    {
    }
}