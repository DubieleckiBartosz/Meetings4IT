using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.ValueObjects;
using Panels.Domain.ScheduledMeetings;

namespace Panels.Domain.DomainServices;

public interface IMeetingDomainService
{
    Meeting Creation(
        ScheduledMeeting? schedule,
        UserInfo organizer,
        MeetingCategory category,
        Description description,
        Address address,
        DateRange date,
        bool isPublic,
        int? maxInvitations);
}