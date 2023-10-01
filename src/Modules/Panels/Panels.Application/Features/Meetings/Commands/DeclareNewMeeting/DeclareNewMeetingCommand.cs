using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters;

namespace Panels.Application.Features.Meetings.Commands.DeclareNewMeeting;

public class DeclareNewMeetingCommand : ICommand<Response<int>>
{
    public string Description { get; }
    public string City { get; }
    public string Street { get; }
    public string NumberStreet { get; }
    public bool IsPublic { get; }
    public int? MaxInvitations { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }
    public int IndexCategory { get; }
    public bool HasPanelVisibility { get; }

    public DeclareNewMeetingCommand(DeclareNewMeetingParameters parameters)
    {
        Description = parameters.Description;
        City = parameters.City;
        Street = parameters.Street;
        NumberStreet = parameters.NumberStreet;
        IsPublic = parameters.IsPublic;
        MaxInvitations = parameters.MaxInvitations;
        StartDate = parameters.StartDate;
        EndDate = parameters.EndDate;
        IndexCategory = parameters.IndexCategory;
        HasPanelVisibility = parameters.HasPanelVisibility;
    }
}