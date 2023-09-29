using Dapper;
using Panels.Application.Contracts.ReadRepositories;
using Panels.Application.Models.DataAccessObjects.Meetings;

namespace Panels.Infrastructure.Repositories.ReadRepositories;

public class MeetingReadRepository : IMeetingReadRepository
{
    public async Task<List<MeetingsSearchResultDao>?> GetMeetingsBySearchAsync(MeetingsSearchDao searchDao)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@organizerName", searchDao.OrganizerName);
        parameters.Add("@from", searchDao.From);
        parameters.Add("@to", searchDao.To);
        parameters.Add("@public", searchDao.Public);
        parameters.Add("@city", searchDao.City);
        parameters.Add("@street", searchDao.Street);
        parameters.Add("@status", searchDao.Status);
        parameters.Add("@category", searchDao.Category);
        parameters.Add("@sortType", searchDao.SortType);
        parameters.Add("@sortName", searchDao.SortName);
        parameters.Add("@pageNumber", searchDao.PageNumber);
        parameters.Add("@pageSize", searchDao.PageSize);

        return null;
    }
}