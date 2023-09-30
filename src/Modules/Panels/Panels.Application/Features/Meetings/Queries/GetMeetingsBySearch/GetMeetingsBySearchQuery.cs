using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Search;
using Meetings4IT.Shared.Implementations.Tools;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters;
using Panels.Application.Models.Views;

namespace Panels.Application.Features.Meetings.Queries.GetMeetingsBySearch;

public record GetMeetingsBySearchQuery : IQuery<Response<ResponseSearchList<MeetingSearchViewModel>>>
{
    public string? OrganizerName { get; }
    public DateTime? From { get; }
    public DateTime? To { get; }
    public bool? Public { get; }
    public string? City { get; }
    public string? Street { get; }
    public int? Status { get; }
    public int? Category { get; }
    public SortModel Sort { get; }
    public BaseSearchQuery Search { get; }

    public GetMeetingsBySearchQuery(GetMeetingsBySearchParameters parameters)
    {
        OrganizerName = parameters.OrganizerName;
        From = parameters.From;
        To = parameters.To;
        Public = parameters.Public;
        City = parameters.City;
        Street = parameters.Street;
        Status = parameters.Status;
        Category = parameters.Category;
        Sort = parameters.CheckOrAssignSortModel();
        Search = new BaseSearchQuery(parameters.PageNumber, parameters.PageSize);
    }
}