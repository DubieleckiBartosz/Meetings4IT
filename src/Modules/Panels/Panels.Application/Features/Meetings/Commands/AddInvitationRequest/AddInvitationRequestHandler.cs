using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.AddInvitationRequest;

public class AddInvitationRequestHandler : ICommandHandler<AddInvitationRequestCommand, Response>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ICurrentUser _currentUser;

    public AddInvitationRequestHandler(IMeetingRepository meetingRepository, ICurrentUser currentUser)
    {
        _meetingRepository = meetingRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(AddInvitationRequestCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _meetingRepository.GetMeetingWithInvitationsAndRequestsByIdAsync(request.MeetingId, cancellationToken);
        if (meeting == null)
        {
            throw new NotFoundException($"Meeting {request.MeetingId} not found.");
        }

        var userId = _currentUser.UserId;
        var creatorName = _currentUser.UserName;

        meeting.AddRequestInvitation(userId, creatorName);

        _meetingRepository.UpdateMeeting(meeting);

        await _meetingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}