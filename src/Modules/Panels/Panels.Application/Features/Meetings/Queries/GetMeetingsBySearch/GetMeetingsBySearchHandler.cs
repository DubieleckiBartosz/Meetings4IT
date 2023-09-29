using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.ReadRepositories;
using Panels.Application.Models.DataAccessObjects.Meetings;
using Panels.Application.Models.Views;

namespace Panels.Application.Features.Meetings.Queries.GetMeetingsBySearch;

public class GetMeetingsBySearchHandler : IQueryHandler<GetMeetingsBySearchQuery, Response<List<MeetingSearchViewModel>>>
{
    private readonly IMeetingReadRepository _meetingReadRepository;
    private readonly ICurrentUser _currentUser;

    public GetMeetingsBySearchHandler(IMeetingReadRepository meetingReadRepository, ICurrentUser currentUser)
    {
        _meetingReadRepository = meetingReadRepository ?? throw new ArgumentNullException(nameof(meetingReadRepository));
        _currentUser = currentUser;
    }

    public async Task<Response<List<MeetingSearchViewModel>>> Handle(GetMeetingsBySearchQuery request, CancellationToken cancellationToken)
    {
        MeetingsSearchDao search = request;
        var result = await _meetingReadRepository.GetMeetingsBySearchAsync(search);

        if (result == null)
        {
            throw new NotFoundException("The list of meetings taken by the search is empty");
        }

        List<MeetingSearchViewModel> viewData = new List<MeetingSearchViewModel>(result.Select(_ => (MeetingSearchViewModel)_));

        return Response<List<MeetingSearchViewModel>>.Ok(viewData);
    }
}