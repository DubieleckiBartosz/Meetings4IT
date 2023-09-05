namespace Panels.Domain.Meetings.Exceptions;

public class MeetingOverlapException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public MeetingOverlapException(int scheduledMeetingId) : base($"Meeting overlap with meeting {scheduledMeetingId}")
    {
    }
}