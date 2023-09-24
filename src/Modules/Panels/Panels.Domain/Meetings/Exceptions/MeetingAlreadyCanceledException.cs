using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MeetingAlreadyCanceledException : BaseException
{
    public MeetingAlreadyCanceledException(int meetingId) : base($"Meeting {meetingId} is canceled")
    {
    }
}