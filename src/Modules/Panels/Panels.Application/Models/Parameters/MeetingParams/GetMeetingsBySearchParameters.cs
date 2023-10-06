using Meetings4IT.Shared.Implementations.Search;
using Meetings4IT.Shared.Implementations.Search.SearchParameters;
using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.MeetingParams;

public class GetMeetingsBySearchParameters : BaseSearchQueryParameters, IFilterModel
{
    public string? OrganizerName { get; }
    public DateTime? From { get; }
    public DateTime? To { get; }
    public bool? Public { get; }
    public string? City { get; }
    public string? Street { get; }
    public int? Status { get; set; }
    public int? Category { get; set; }
    public SortModelParameters Sort { get; set; }

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