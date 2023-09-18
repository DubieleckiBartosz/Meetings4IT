using Meetings4IT.Shared.Abstractions.Kernel;
using Panels.Domain.Meetings;

namespace Panels.Application.Contracts.Repositories;

public interface IMeetingRepository : IRepository<Meeting>
{
    Task<Meeting> GetMeetingByIdAsync(int meetingId, CancellationToken cancellationToken = default);

    Task CreateNewMeetingAsync(Meeting meeting, CancellationToken cancellationToken = default);
}