using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingEndedException : BaseException
{
    public MeetingEndedException(int meetingId) : base($"Meeting {meetingId} ended.")
    {
    }
}