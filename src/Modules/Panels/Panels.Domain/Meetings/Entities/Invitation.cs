using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Generators;
using Panels.Domain.Meetings.Exceptions.InvitationExceptions;
using Panels.Domain.Meetings.Statuses;
using Panels.Domain.Meetings.ValueObjects;

namespace Panels.Domain.Meetings.Entities;

public class Invitation : Entity
{
    public InvitationCode Code { get; }
    public Email Email { get; }
    public InvitationStatus Status { get; private set; }
    public Date ExpirationDate { get; private set; }
    private bool IsExpired => ExpirationDate.Value <= Clock.CurrentDate();

    private Invitation()
    { }

    internal Invitation(Email email, Date expirationDate, InvitationCode code)
    {
        Email = email;
        Code = code;
        ExpirationDate = expirationDate;
        Status = InvitationStatus.Pending;
    }

    internal void Accept()
    {
        if (IsExpired)
        {
            throw new InvitationExpiredException(ExpirationDate);
        }

        Status = InvitationStatus.Accepted;
    }

    internal void Reject()
    {
        if (IsExpired)
        {
            throw new InvitationExpiredException(ExpirationDate);
        }

        Status = InvitationStatus.Rejected;
    }

    internal void Cancel()
    {
        var statusToCancel = Status == InvitationStatus.Accepted || Status == InvitationStatus.Pending;
        if (statusToCancel)
        {
            throw new InvitationInvalidStatusException(Email, Status);
        }

        Status = InvitationStatus.Canceled;
    }

    internal void Expire()
    {
        if (ExpirationDate > Clock.CurrentDateObject())
        {
            throw new InvitationNotExpiredException(ExpirationDate);
        }

        Status = InvitationStatus.Expired;
    }
}