using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.Exceptions;
using Panels.Domain.Meetings.ValueObjects;
using Panels.Domain.ScheduledMeetings;

namespace Panels.Domain.DomainServices;

public class MeetingDomainService : IMeetingDomainService
{
    public Meeting Creation(
        ScheduledMeeting? schedule,
        UserInfo organizer,
        MeetingCategory category,
        Description description,
        Address address,
        DateRange date,
        bool isPublic,
        int? maxInvitations)
    {
        var scheduledMeeting = schedule?.UpcomingMeetings.SingleOrDefault(_ => IsOverlappingWithExisting(date, _.MeetingDateRange));

        if (scheduledMeeting != null)
        {
            throw new MeetingOverlapException(scheduledMeeting.MeetingId);
        }

        return Meeting.Create(
            organizer,
            category,
            description,
            address,
            date,
            isPublic,
            maxInvitations);
    }

    private bool IsOverlappingWithExisting(DateRange newRange, DateRange existingRange)
    {
        if (existingRange.EndDate.HasValue)
        {
            if (newRange.StartDate <= existingRange.EndDate && existingRange.StartDate <= newRange.EndDate)
            {
                return true; // Overlapping detected
            }
        }

        return false; // No overlapping
    }
}