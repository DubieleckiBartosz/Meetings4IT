using Meetings4IT.Shared.Implementations.Search;
using Meetings4IT.Shared.Implementations.Search.SearchParameters;
using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class GetMeetingsBySearchParameters : BaseSearchQueryParameters, IFilterModel
{
    public string? OrganizerName { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
    public bool? Public { get; init; }
    public string? City { get; init; }
    public string? Street { get; init; }
    public int? Status { get; init; }
    public int? Category { get; init; }
    public SortModelParameters Sort { get; init; }

    public GetMeetingsBySearchParameters()
    { }

    [JsonConstructor]
    public GetMeetingsBySearchParameters(
        string? organizerName,
        DateTime? from,
        DateTime? to,
        bool? @public,
        string? city,
        string? street,
        int? status,
        int? category,
        int pageNumber,
        int pageSize,
        SortModelParameters sort)
    {
        OrganizerName = organizerName;
        From = from;
        To = to;
        Public = @public;
        City = city;
        Street = street;
        Status = status;
        Category = category;
        Sort = sort;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}