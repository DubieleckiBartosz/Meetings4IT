using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions.InvitationRequestExceptions;

public class InvitationRequestExistsException : BaseException
{
    public InvitationRequestExistsException(int meetingId, string userRequestCreatorId)
        : base($"The request already exists. [MeetingId {meetingId}, RequestCreator {userRequestCreatorId}]")
    {
    }
}