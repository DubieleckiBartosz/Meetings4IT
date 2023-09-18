using Panels.Domain.ScheduledMeetings;

namespace Panels.Application.Contracts.Repositories;

public interface IScheduledMeetingRepository
{
    Task CreateScheduledMeetingAsync(ScheduledMeeting scheduledMeeting, CancellationToken cancellationToken = default);

    void UpdateScheduledMeeting(ScheduledMeeting scheduledMeeting, CancellationToken cancellationToken = default);

    Task<ScheduledMeeting?> GetScheduledMeetingAsync(string owner, CancellationToken cancellationToken = default);
}