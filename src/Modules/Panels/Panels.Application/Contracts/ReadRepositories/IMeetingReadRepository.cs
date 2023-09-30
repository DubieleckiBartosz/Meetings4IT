using Panels.Application.Models.DataAccessObjects.Meetings;

namespace Panels.Application.Contracts.ReadRepositories;

public interface IMeetingReadRepository
{
    Task<List<MeetingsSearchResultDao>?> GetMeetingsBySearchAsync(MeetingsSearchDao searchDao, string? userId);
}