using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Search;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.ReadRepositories;
using Panels.Application.Models.DataAccessObjects.Meetings;
using Panels.Application.Models.Views;

namespace Panels.Application.Features.Meetings.Queries.GetMeetingsBySearch;

public class GetMeetingsBySearchHandler : IQueryHandler<GetMeetingsBySearchQuery, Response<ResponseSearchList<MeetingSearchViewModel>>>
{
    private readonly IMeetingReadRepository _meetingReadRepository;
    private readonly ICurrentUser _currentUser;

    public GetMeetingsBySearchHandler(IMeetingReadRepository meetingReadRepository, ICurrentUser currentUser)
    {
        _meetingReadRepository = meetingReadRepository ?? throw new ArgumentNullException(nameof(meetingReadRepository));
        _currentUser = currentUser;
    }

    public async Task<Response<ResponseSearchList<MeetingSearchViewModel>>> Handle(GetMeetingsBySearchQuery request, CancellationToken cancellationToken)
    {
        MeetingsSearchDao search = request;

        var userId = _currentUser.UserId;
        var result = await _meetingReadRepository.GetMeetingsBySearchAsync(search, userId);

        if (result == null)
        {
            throw new NotFoundException("The list of meetings taken by the search is empty");
        }

        var cnt = result[0].TotalCount;
        List<MeetingSearchViewModel> viewData = new List<MeetingSearchViewModel>(result.Select(_ => (MeetingSearchViewModel)_));

        var responseSearch = ResponseSearchList<MeetingSearchViewModel>.Create(viewData, cnt)!;
        return Response<ResponseSearchList<MeetingSearchViewModel>>.Ok(responseSearch);
    }
}