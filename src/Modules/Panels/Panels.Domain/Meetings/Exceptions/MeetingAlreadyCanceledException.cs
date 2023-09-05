namespace Panels.Domain.Meetings.Exceptions;

public class MeetingAlreadyCanceledException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public MeetingAlreadyCanceledException(int meetingId) : base($"Meeting {meetingId} is canceled")
    {
    }
}