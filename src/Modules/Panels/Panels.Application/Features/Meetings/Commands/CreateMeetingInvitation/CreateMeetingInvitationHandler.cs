using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Meetings.ValueObjects;

namespace Panels.Application.Features.Meetings.Commands.CreateMeetingInvitation;

public class CreateMeetingInvitationHandler : ICommandHandler<CreateMeetingInvitationCommand, Response<int>>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;

    public CreateMeetingInvitationHandler(
        IMeetingRepository meetingRepository,
        IUserRepository userRepository,
        ICurrentUser currentUser)
    {
        _meetingRepository = meetingRepository;
        _userRepository = userRepository;
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
        Email? email = request.EmailInvitationRecipient;
        NameInvitationRecipient recipientName = request.NameInvitationRecipient!;

        var result = await _userRepository.GetUserByNameNTAsync(recipientName);

        if (email == null)
        {
            if (result == null)
            {
                throw new NotFoundException($"User {recipientName.Value} not found." +
                    $" Please provide an email address or a valid existing user name.");
            }

            email = result.Email;
        }

        var invitation = meeting.CreateNewInvitation(email, expiration, recipientName, result?.Identifier);

        _meetingRepository.UpdateMeeting(meeting);

        await _meetingRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response<int>.Ok(invitation.Id);
    }
}