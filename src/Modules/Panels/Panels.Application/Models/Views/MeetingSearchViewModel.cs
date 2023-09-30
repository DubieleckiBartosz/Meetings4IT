using Panels.Application.Models.DataAccessObjects.Meetings;

namespace Panels.Application.Models.Views;

public class MeetingSearchViewModel
{
    public int MeetingId { get; }
    public string City { get; } = default!;
    public string Status { get; } = default!;
    public DateTime StartDate { get; } = default!;
    public DateTime? EndDate { get; }
    public DateTime? CancellationDate { get; init; }
    public string Category { get; } = default!;
    public string OrganizerName { get; } = default!;
    public string OrganizerId { get; } = default!;
    public DateTime Created { get; }

    private MeetingSearchViewModel(
        int meetingId,
        string city,
        string status,
        DateTime startDate,
        DateTime? endDate,
        DateTime? cancellationDate,
        string category,
        string organizerName,
        string organizerId,
        DateTime created)
    {
        MeetingId = meetingId;
        City = city;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
        CancellationDate = cancellationDate;
        Category = category;
        OrganizerName = organizerName;
        OrganizerId = organizerId;
        Created = created;
    }

    public static implicit operator MeetingSearchViewModel(
        MeetingsSearchResultDao meetingsSearchResultDao)
    {
        return new MeetingSearchViewModel(
            meetingsSearchResultDao.MeetingId,
            meetingsSearchResultDao.City,
            meetingsSearchResultDao.Status,
            meetingsSearchResultDao.StartDate,
            meetingsSearchResultDao.CancellationDate,
            meetingsSearchResultDao.EndDate,
            meetingsSearchResultDao.Category,
            meetingsSearchResultDao.OrganizerName,
            meetingsSearchResultDao.OrganizerId,
            meetingsSearchResultDao.Created);
    }
}