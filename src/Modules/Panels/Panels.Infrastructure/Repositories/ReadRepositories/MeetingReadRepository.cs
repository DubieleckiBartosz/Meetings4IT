using Dapper;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.Extensions.Options;
using Panels.Application.Contracts.ReadRepositories;
using Panels.Application.Models.DataAccessObjects.Meetings;
using Serilog;

namespace Panels.Infrastructure.Repositories.ReadRepositories;

public class MeetingReadRepository : BaseRepository, IMeetingReadRepository
{
    public MeetingReadRepository(IOptions<DapperOptions> options, ILogger logger) : base(options!.Value.DefaultConnection, logger)
    {
    }

    public async Task<List<MeetingsSearchResultDao>?> GetMeetingsBySearchAsync(MeetingsSearchDao searchDao, string? userId)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@userId", userId);
        parameters.Add("@organizerName", searchDao.OrganizerName);
        parameters.Add("@from", searchDao.From);
        parameters.Add("@to", searchDao.To);
        parameters.Add("@isPublic", searchDao.Public);
        parameters.Add("@city", searchDao.City);
        parameters.Add("@street", searchDao.Street);
        parameters.Add("@status", searchDao.Status);
        parameters.Add("@category", searchDao.Category);
        parameters.Add("@sortType", searchDao.SortType);
        parameters.Add("@sortName", searchDao.SortName);
        parameters.Add("@pageNumber", searchDao.PageNumber);
        parameters.Add("@pageSize", searchDao.PageSize);

        var result = await this.QueryAsync<MeetingsSearchResultDao>("meetings_getBySearch_S", parameters, System.Data.CommandType.StoredProcedure);
        return result?.ToList();
    }
}