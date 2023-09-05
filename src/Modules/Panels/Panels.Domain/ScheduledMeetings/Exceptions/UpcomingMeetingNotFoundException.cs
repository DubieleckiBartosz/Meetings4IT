using System.Net;

namespace Panels.Domain.ScheduledMeetings.Exceptions;

public class UpcomingMeetingNotFoundException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public UpcomingMeetingNotFoundException(int upcomingMeetingId) : base($"Meeting {upcomingMeetingId} not found.", HttpStatusCode.NotFound)
    {
    }
}