using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.DeleteInvitationRequest;

public class DeleteInvitationRequestHandler : ICommandHandler<DeleteInvitationRequestCommand, Response>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ICurrentUser _currentUser;

    public DeleteInvitationRequestHandler(IMeetingRepository meetingRepository, ICurrentUser currentUser)
    {
        _meetingRepository = meetingRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(DeleteInvitationRequestCommand request, CancellationToken cancellationToken)
    {
        var meetingResult = await _meetingRepository.GetMeetingWithInvitationRequestsByIdAsync(request.MeetingId, cancellationToken);
        if (meetingResult == null)
        {
            throw new NotFoundException($"Meeting {request.MeetingId} not found.");
        }

        var userId = _currentUser.UserId;

        meetingResult.DeleteInvitationRequest(userId);

        _meetingRepository.UpdateMeeting(meetingResult);
        await _meetingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}