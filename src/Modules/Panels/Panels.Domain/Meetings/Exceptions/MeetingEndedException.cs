using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingEndedException : BusinessException
{
    public MeetingEndedException(int meetingId) : base($"Meeting {meetingId} ended.")
    {
    }
}