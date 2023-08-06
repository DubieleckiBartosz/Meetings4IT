using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingAlreadyCanceledException : BusinessException
{
    public MeetingAlreadyCanceledException(int meetingId) : base($"Meeting {meetingId} is canceled")
    {
    }
}