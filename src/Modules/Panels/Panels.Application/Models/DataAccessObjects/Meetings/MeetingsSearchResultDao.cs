namespace Panels.Application.Models.DataAccessObjects.Meetings;

public class MeetingsSearchResultDao
{
    public int MeetingId { get; init; }
    public string City { get; init; }
    public string Status { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public DateTime? CancellationDate { get; init; }
    public DateTime Created { get; init; }
    public string Category { get; init; }
    public string OrganizerId { get; init; }
    public string OrganizerName { get; init; }
    public int? MaxInvitations { get; }
    public int AcceptedInvitations { get; }
    public int TotalCount { get; init; }
}