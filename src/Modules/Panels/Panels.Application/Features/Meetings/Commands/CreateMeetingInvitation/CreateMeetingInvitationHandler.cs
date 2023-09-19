using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.CreateMeetingInvitation;

public class CreateMeetingInvitationHandler : ICommandHandler<CreateMeetingInvitationCommand, Response<int>>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ICurrentUser _currentUser;

    public CreateMeetingInvitationHandler(
        IMeetingRepository meetingRepository,
        ICurrentUser currentUser)
    {
        _meetingRepository = meetingRepository;
        _currentUser = currentUser;
    }

    public async Task<Response<int>> Handle(CreateMeetingInvitationCommand request, CancellationToken cancellationToken)
    {
        var organizerId = _currentUser.UserId;
        var meetingId = request.MeetingId;
        var meeting = await _meetingRepository.GetMeetingWithInvitationsByIdAsync(meetingId);
        if (meeting == null || meeting.Organizer.Identifier != organizerId)
        {
            throw new NotFoundException($"Meeting {meetingId} not found.");
        }

        Date expiration = request.InvitationExpirationDate;
        Email recipient = request.InvitationRecipient!;

        var invitation = meeting.CreateNewInvitation(recipient, expiration);

        _meetingRepository.UpdateMeetingAsync(meeting);

        await _meetingRepository.UnitOfWork.SaveAsync();

        return Response<int>.Ok(invitation.Id);
    }
}