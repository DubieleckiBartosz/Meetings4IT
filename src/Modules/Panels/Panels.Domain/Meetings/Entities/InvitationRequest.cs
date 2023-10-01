using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Meetings.Statuses;

namespace Panels.Domain.Meetings.Entities;

public class InvitationRequest : Entity
{
    public int MeetingId { get; }
    public UserInfo RequestCreator { get; }
    public RequestStatus Status { get; private set; }
    public string? ReasonRejection { get; private set; }
    public DateTime Created { get; }
    public DateTime LastModified { get; private set; }

    private InvitationRequest()
    { }

    private InvitationRequest(
        int meetingId,
        UserInfo requestCreator)
    {
        MeetingId = meetingId;
        RequestCreator = requestCreator;
        Status = RequestStatus.Pending;

        var now = Clock.CurrentDate();
        Created = now;
        LastModified = now;
    }

    public static InvitationRequest Create(int meetingId, UserInfo requestCreator)
    {
        return new InvitationRequest(meetingId, requestCreator);
    }

    public void Accept()
    {
        Status = RequestStatus.Accepted;
        LastModified = Clock.CurrentDate();
    }

    public void Reject(string? reasonRejection = null)
    {
        Status = RequestStatus.Rejected;
        ReasonRejection = reasonRejection;
        LastModified = Clock.CurrentDate();
    }
}