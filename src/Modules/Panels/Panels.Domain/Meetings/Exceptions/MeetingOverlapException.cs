using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingOverlapException : BaseException
{
    public MeetingOverlapException(Guid scheduledMeetingId) : base($"Meeting overlap with meeting {scheduledMeetingId}")
    {
    }
}