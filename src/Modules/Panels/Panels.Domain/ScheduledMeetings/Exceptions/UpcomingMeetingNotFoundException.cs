using Meetings4IT.Shared.Abstractions.Exceptions;
using System.Net;

namespace Panels.Domain.ScheduledMeetings.Exceptions;

public class UpcomingMeetingNotFoundException : BaseException
{
    public UpcomingMeetingNotFoundException(Guid upcomingMeetingId) : base($"Meeting {upcomingMeetingId} not found.", HttpStatusCode.NotFound)
    {
    }
}