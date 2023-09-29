using Panels.Application.Models.DataAccessObjects.Meetings;

namespace Panels.Application.Models.Views;

public class MeetingSearchViewModel
{
    public string City { get; } = default!;
    public string Status { get; } = default!;
    public DateTime StartDate { get; } = default!;
    public DateTime EndDate { get; } = default!;
    public string Category { get; } = default!;
    public string OrganizerName { get; } = default!;

    private MeetingSearchViewModel(
        string city,
        string status,
        DateTime startDate,
        DateTime endDate,
        string category,
        string organizerName)
    {
        City = city;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        Category = category;
        OrganizerName = organizerName;
    }

    public static implicit operator MeetingSearchViewModel(MeetingsSearchResultDao meetingsSearchResultDao)
    {
        return new MeetingSearchViewModel(
            meetingsSearchResultDao.City,
            meetingsSearchResultDao.Status,
            meetingsSearchResultDao.StartDate,
            meetingsSearchResultDao.EndDate,
            meetingsSearchResultDao.Category,
            meetingsSearchResultDao.OrganizerName);
    }
}