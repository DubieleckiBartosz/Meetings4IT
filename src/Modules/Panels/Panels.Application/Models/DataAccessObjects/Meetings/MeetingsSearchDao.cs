using Panels.Application.Features.Meetings.Queries.GetMeetingsBySearch;

namespace Panels.Application.Models.DataAccessObjects.Meetings;

public class MeetingsSearchDao
{
    public string? OrganizerName { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
    public bool? Public { get; init; }
    public string? City { get; init; }
    public string? Street { get; init; }
    public int? Status { get; init; }
    public int? Category { get; init; }
    public string SortType { get; init; } = default!;
    public string SortName { get; init; } = default!;
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public static implicit operator MeetingsSearchDao(GetMeetingsBySearchQuery searchQuery)
    {
        return new MeetingsSearchDao()
        {
            OrganizerName = searchQuery.OrganizerName,
            From = searchQuery.From,
            To = searchQuery.To,
            Public = searchQuery.Public,
            City = searchQuery.City,
            Street = searchQuery.Street,
            Status = searchQuery.Status,
            Category = searchQuery.Category,
            SortType = searchQuery.Sort.Type,
            SortName = searchQuery.Sort.Name,
            PageNumber = searchQuery.Search.PageNumber,
            PageSize = searchQuery.Search.PageSize
        };
    }
}