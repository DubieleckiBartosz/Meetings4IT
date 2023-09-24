using Meetings4IT.Shared.Abstractions.Kernel;
using Panels.Domain.Meetings;

namespace Panels.Application.Contracts.Repositories;

public interface IMeetingRepository : IRepository<Meeting>
{
    Task CreateNewMeetingAsync(Meeting meeting, CancellationToken cancellationToken = default);

    void UpdateMeeting(Meeting meeting);

    Task<Meeting?> GetMeetingWithInvitationsByIdAsync(int meetingId, CancellationToken cancellationToken = default);

    Task<List<Meeting>?> GetAllCompletedMeetingsForStatusUpdatesAsync(CancellationToken cancellationToken = default);
}