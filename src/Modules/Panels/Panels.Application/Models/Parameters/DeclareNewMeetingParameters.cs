using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class DeclareNewMeetingParameters
{
    public string Description { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string NumberStreet { get; init; }
    public bool IsPublic { get; init; }
    public int? MaxInvitations { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int IndexCategory { get; init; }
    public bool HasPanelVisibility { get; init; }

    public DeclareNewMeetingParameters()
    {
    }

    [JsonConstructor]
    public DeclareNewMeetingParameters(
        string description,
        string city,
        string street,
        string numberStreet,
        bool isPublic,
        int? maxInvitations,
        DateTime startDate,
        DateTime? endDate,
        int indexCategory,
        bool hasPanelVisibility)
    {
        Description = description;
        City = city;
        Street = street;
        NumberStreet = numberStreet;
        IsPublic = isPublic;
        MaxInvitations = maxInvitations;
        StartDate = startDate;
        EndDate = endDate;
        IndexCategory = indexCategory;
        HasPanelVisibility = hasPanelVisibility;
    }
}