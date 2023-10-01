using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationRequestExceptions;

public class InvitationRequestNotFoundException : BaseException
{
    public InvitationRequestNotFoundException(int meetingId, string userRequestCreatorId)
        : base($"No request found for user. [MeetingId {meetingId}, RequestCreator {userRequestCreatorId}]")
    {
    }
}