using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.DomainServices;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.ValueObjects;

namespace Panels.Application.Features.Meetings.Commands.DeclareNewMeeting;

public class DeclareNewMeetingHandler : ICommandHandler<DeclareNewMeetingCommand, Response<int>>
{
    private readonly IMeetingDomainService _meetingDomainService;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IScheduledMeetingRepository _scheduledMeetingRepository;
    private readonly IMeetingCategoryRepository _meetingCategoryRepository;
    private readonly ICurrentUser _currentUser;

    public DeclareNewMeetingHandler(
        IMeetingDomainService meetingDomainService,
        IMeetingRepository meetingRepository,
        IScheduledMeetingRepository scheduledMeetingRepository,
        IMeetingCategoryRepository meetingCategoryRepository,
        ICurrentUser currentUser)
    {
        _meetingDomainService = meetingDomainService;
        _meetingRepository = meetingRepository;
        _scheduledMeetingRepository = scheduledMeetingRepository;
        _meetingCategoryRepository = meetingCategoryRepository;
        _currentUser = currentUser;
    }

    public async Task<Response<int>> Handle(DeclareNewMeetingCommand request, CancellationToken cancellationToken)
    {
        var organizerId = _currentUser.UserId;
        var organizerName = _currentUser.UserName;

        var scheduledMeeting = await _scheduledMeetingRepository.GetScheduledMeetingAsync(organizerId, cancellationToken);
        var category = await _meetingCategoryRepository.GetMeetingCategoryByIndexAsync(request.IndexCategory, cancellationToken);
        //Exception category

        Address address = Address.Create(request.City, request.Street, request.NumberStreet);
        Description description = request.Description;
        MeetingCategory meetingCategory = new MeetingCategory(request.IndexCategory, category);
        UserInfo organizer = new UserInfo(organizerId, organizerName);
        DateRange dateRange = new DateRange(request.StartDate, request.EndDate);

        var newMeeting = _meetingDomainService.Creation(
            scheduledMeeting,
            organizer,
            meetingCategory,
            description,
            address,
            dateRange,
            request.IsPublic,
            request.MaxInvitations);

        await _meetingRepository.CreateNewMeetingAsync(newMeeting, cancellationToken);

        await _meetingRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response<int>.Ok(newMeeting.Id);
    }
}