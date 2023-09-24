using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingActiveNowException : BaseException
{
    public MeetingActiveNowException(int meetingId) : base($"Meeting {meetingId} is active and operation is not possible in this state")
    {
    }
}