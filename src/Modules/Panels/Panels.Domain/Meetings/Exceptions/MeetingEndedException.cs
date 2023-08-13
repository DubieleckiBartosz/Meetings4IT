using Meetings4IT.Shared.Abstractions.Exceptions; 

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingEndedException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public MeetingEndedException(int meetingId) : base($"Meeting {meetingId} ended.")
    {
    }
}