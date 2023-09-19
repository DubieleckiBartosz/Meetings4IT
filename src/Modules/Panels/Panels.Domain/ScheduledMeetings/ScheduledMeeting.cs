using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.ScheduledMeetings.Events;
using Panels.Domain.ScheduledMeetings.Exceptions;
using Panels.Domain.ScheduledMeetings.ValueObjects;

namespace Panels.Domain.ScheduledMeetings;

public class ScheduledMeeting : Entity, IAggregateRoot
{
    private readonly List<UpcomingMeeting> _upcomingMeetings;
    public UserInfo ScheduleOwner { get; }
    public List<UpcomingMeeting> UpcomingMeetings => _upcomingMeetings;

    private ScheduledMeeting()
    {
        _upcomingMeetings = new();
    }

    private ScheduledMeeting(UserInfo scheduleOwner)
    {
        ScheduleOwner = scheduleOwner;
        _upcomingMeetings = new();
        IncrementVersion();
    }

    public static ScheduledMeeting CreateMeetingSchedule(UserInfo scheduleOwner) => new(scheduleOwner);

    public void NewUpcomingMeeting(UpcomingMeeting newUpcomingMeeting)
    {
        _upcomingMeetings.Add(newUpcomingMeeting);
        IncrementVersion();
    }

    public void RevokeMeeting(Guid meetingId)
    {
        var upcomingMeeting = _upcomingMeetings.SingleOrDefault(_ => _.MeetingId == meetingId);
        if (upcomingMeeting == null)
        {
            throw new UpcomingMeetingNotFoundException(meetingId);
        }

        _upcomingMeetings.Remove(upcomingMeeting);

        this.AddEvent(UpcomingMeetingRevoked.Create(meetingId, ScheduleOwner.Name));
        IncrementVersion();
    }
}