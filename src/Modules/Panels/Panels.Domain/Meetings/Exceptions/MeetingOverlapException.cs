using Meetings4IT.Shared.Abstractions.Exceptions; 

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingOverlapException : BusinessException
{
    public MeetingOverlapException(int scheduledMeetingId) : base($"Meeting overlap with meeting {scheduledMeetingId}")
    {
    }
}