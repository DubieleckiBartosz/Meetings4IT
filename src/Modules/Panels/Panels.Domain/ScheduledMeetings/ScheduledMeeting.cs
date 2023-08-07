using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects; 
using Panels.Domain.ScheduledMeetings.Events;
using Panels.Domain.ScheduledMeetings.Exceptions;
using Panels.Domain.ScheduledMeetings.ValueObjects;

namespace Panels.Domain.ScheduledMeetings;

public class ScheduledMeeting : Entity, IAggregateRoot
{
    private readonly HashSet<UpcomingMeeting> _upcomingMeeting = new(); 
    public Email ScheduleOwner { get; } 
    public List<UpcomingMeeting> UpcomingMeetings => _upcomingMeeting.ToList();

    private ScheduledMeeting(Email scheduleOwner)
    {
        ScheduleOwner = scheduleOwner;
        IncrementVersion();
    }

    public static ScheduledMeeting CreateMeetingSchedule(Email creator) => new(creator);

    public void NewUpcomingMeeting(UpcomingMeeting newUpcomingMeeting)
    {
        _upcomingMeeting.Add(newUpcomingMeeting); 
        IncrementVersion();
    }

    public void RevokeMeeting(int meetingId)
    {
        var upcomingMeeting = _upcomingMeeting.SingleOrDefault(_ => _.MeetingId == meetingId);
        if (upcomingMeeting == null)
        {
            throw new UpcomingMeetingNotFoundException(meetingId);
        }

        _upcomingMeeting.Remove(upcomingMeeting);

        this.AddEvent(UpcomingMeetingRevoked.Create(meetingId, ScheduleOwner)); 
        IncrementVersion();
    }
}