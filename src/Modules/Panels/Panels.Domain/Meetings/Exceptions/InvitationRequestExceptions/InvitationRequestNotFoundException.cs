using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationRequestExceptions;

public class InvitationRequestNotFoundException : BaseException
{
    public InvitationRequestNotFoundException(int meetingId, int invitationRequestId)
        : base($"Invitation request not found. [MeetingId {meetingId}, InvitationRequestId {invitationRequestId}]")
    {
    }

    public InvitationRequestNotFoundException(int meetingId, string invitationRequestCreator)
        : base($"Invitation request not found. [MeetingId {meetingId}, InvitationRequestCreator {invitationRequestCreator}]")
    {
    }
}