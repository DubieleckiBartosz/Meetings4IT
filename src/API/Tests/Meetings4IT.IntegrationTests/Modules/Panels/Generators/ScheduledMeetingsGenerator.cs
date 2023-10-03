using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Meetings;
using Panels.Domain.ScheduledMeetings;
using Panels.Domain.ScheduledMeetings.ValueObjects;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Generators;

public static class ScheduledMeetingsGenerator
{
    public static ScheduledMeeting GetScheduledMeeting(bool withUpcoming = true)
    {
        var schedule = ScheduledMeeting.CreateMeetingSchedule(new UserInfo(GlobalUserData.Identifier, GlobalUserData.UserName));

        if (withUpcoming)
        {
            var dateFutre = Clock.CurrentDate().AddDays(1);
            var upcomingMeeting = UpcomingMeeting.Create(MeetingId.Create(), new DateRange(dateFutre, null));

            schedule.NewUpcomingMeeting(upcomingMeeting);
        }

        return schedule;
    }
}