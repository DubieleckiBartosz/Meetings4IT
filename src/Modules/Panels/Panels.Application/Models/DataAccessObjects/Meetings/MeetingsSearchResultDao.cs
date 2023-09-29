namespace Panels.Application.Models.DataAccessObjects.Meetings;

public class MeetingsSearchResultDao
{
    public string City { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Category { get; set; }
    public string OrganizerName { get; set; }
}