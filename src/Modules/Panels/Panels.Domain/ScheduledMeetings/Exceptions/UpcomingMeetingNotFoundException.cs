using Meetings4IT.Shared.Domain.DomainExceptions;
using System.Net;

namespace Panels.Domain.ScheduledMeetings.Exceptions;

public class UpcomingMeetingNotFoundException : BusinessException
{
    public UpcomingMeetingNotFoundException(int upcomingMeetingId) : base($"Meeting {upcomingMeetingId} not found.", HttpStatusCode.NotFound)
    {
    }
}