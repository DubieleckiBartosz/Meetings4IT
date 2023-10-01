using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Generators;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.Entities;
using Panels.Domain.Meetings.Events;
using Panels.Domain.Meetings.Exceptions;
using Panels.Domain.Meetings.Exceptions.InvitationExceptions;
using Panels.Domain.Meetings.Exceptions.InvitationRequestExceptions;
using Panels.Domain.Meetings.Statuses;
using Panels.Domain.Meetings.ValueObjects;
using System.Linq;

namespace Panels.Domain.Meetings;

public class Meeting : Entity, IAggregateRoot
{
    private readonly List<Invitation> _invitations;
    private readonly List<Comment> _comments;
    private readonly List<MeetingImage> _images;
    private readonly List<InvitationRequest> _requests;

    /// <summary>
    /// This property is responsible for upcoming meetings.
    /// We don't have access to the int Id when creating a meeting
    /// </summary>
    public MeetingId ExplicitMeetingId { get; }

    public UserInfo Organizer { get; }
    public int CategoryIndex { get; private set; }

    /// <summary>
    /// We don't add category from UI, but we have categories as seed
    /// </summary>
    public MeetingCategory Category { get; private set; }

    //Anyone can come if public property is true
    public bool IsPublic { get; private set; }

    /// <summary>
    /// Property is responsible for the visibility of meeting on panel.
    /// This is always true for a basic subscription or free account
    /// </summary>
    public bool HasPanelVisibility { get; private set; }

    public int? MaxInvitations { get; private set; }

    /// <summary>
    /// This property can be calculated by the invitation list,
    /// but we choose this approach for faster queries
    /// </summary>
    public int AcceptedInvitations { get; private set; }

    public DateTime Created { get; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public MeetingCancellation? Cancellation { get; private set; }
    public DateRange Date { get; private set; }
    public MeetingStatus Status { get; private set; }

    /// <summary>
    /// This property gets information about the status of the meeting at this time
    /// - probably redundant, but we'll leave it until we figure out how to update the "completed" status.
    /// </summary>
    public bool IsCompletedNow => Date.StartDate <= Clock.CurrentDate() && Status != MeetingStatus.Cancelled;

    private int PendingInvitations => NumberInvitationsByStatus(InvitationStatus.Pending);

    private int NumberInvitationsByStatus(InvitationStatus status)
    {
        return _invitations.Count(_ => _.Status == status);
    }

    private Meeting()
    {
        this._invitations = new();
        this._images = new();
        this._comments = new();
        this._requests = new();
    }

    //This constructor is special for optimization
    public Meeting(int id, MeetingStatus meetingStatus) => (Id, Status) = (id, meetingStatus);

    private Meeting(
        UserInfo organizer,
        MeetingCategory category,
        Description description,
        Address address,
        DateRange date,
        bool isPublic,
        bool hasPanelVisibility,
        int? maxInvitations)
    {
        Organizer = organizer;
        CategoryIndex = category.Index;
        Description = description;
        Address = address;
        Date = date;
        IsPublic = isPublic;
        HasPanelVisibility = hasPanelVisibility;
        MaxInvitations = maxInvitations;
        AcceptedInvitations = 0;
        Created = Clock.CurrentDate();
        ExplicitMeetingId = MeetingId.Create();
        Status = MeetingStatus.Active;

        this._invitations = new();
        this._images = new();
        this._comments = new();
        this._requests = new();

        IncrementVersion();

        this.AddEvent(MeetingCreated.Create(this.ExplicitMeetingId, organizer, date));
    }

    public static Meeting Create(
        UserInfo organizer,
        MeetingCategory category,
        Description description,
        Address address,
        DateRange date,
        bool isPublic,
        bool hasPanelVisibility,
        int? maxInvitations = null)
    {
        if (isPublic && maxInvitations.HasValue)
        {
            throw new InvitationAvailabilityMustBeNullException();
        }

        return new Meeting(
            organizer,
            category,
            description,
            address,
            date,
            isPublic,
            hasPanelVisibility,
            maxInvitations);
    }

    public void IncreaseMaxNumberOfInvitations(int newMaxNumberOfInvitations)
    {
        this.CheckIfMeetingOperationIsPossible();

        if (newMaxNumberOfInvitations <= MaxInvitations)
        {
            throw new InvalidIncreaseMaxInvitationsException();
        }

        MaxInvitations = newMaxNumberOfInvitations;
        this.IncrementVersion();
    }

    public void Cancel(string? reason)
    {
        this.CheckIfMeetingOperationIsPossible();
        this.CheckIfMeetingWasCanceled();

        Cancellation = MeetingCancellation.CreateCancellation(reason);
        Status = MeetingStatus.Cancelled;

        var invitations = _invitations
            .Where(_ => _.Status == InvitationStatus.Pending || _.Status == InvitationStatus.Accepted).ToList();

        foreach (var invitation in invitations)
        {
            invitation.Cancel();
        }

        var invitationRecipients = invitations?.Select(_ => _.Email.Value).ToList();

        this.AddEvent(MeetingCanceled.Create(ExplicitMeetingId.ToString(), Organizer.Name, invitationRecipients));
        IncrementVersion();
    }

    public Invitation CreateNewInvitation(
        Email email,
        Date invitationExpirationDate,
        NameInvitationRecipient recipientName,
        string? recipientId = null)
    {
        this.CheckIfMeetingOperationIsPossible();
        this.CheckIfMeetingWasCanceled();

        var acceptedInvitations = AcceptedInvitations;
        if (MaxInvitations != null && (acceptedInvitations == MaxInvitations ||
                                       PendingInvitations + acceptedInvitations == MaxInvitations.Value))
        {
            throw new MaxInvitationsReachedException(MaxInvitations.Value, acceptedInvitations, PendingInvitations);
        }

        var invitationAlreadyExists = _invitations.FirstOrDefault(_ => _.Email == email);
        if (invitationAlreadyExists != null)
        {
            throw new InvitationAlreadyExistsException(email);
        }

        var code = string.Empty;
        while (code == string.Empty)
        {
            var newCode = InvitationCode.Create(InvitationCodeGenerator.GenerateCode(this.Id));
            var codeExists = _invitations.FirstOrDefault(_ => _.Code == newCode);
            if (codeExists == null)
            {
                code = newCode;
            }
        }

        var invitation = new Invitation(email, invitationExpirationDate, code, recipientName);
        _invitations.Add(invitation);

        if (recipientId != null)
        {
            var requestInvitation = this._requests.FirstOrDefault(_ => _.RequestCreator.Identifier == recipientId);
            if (requestInvitation != null)
            {
                requestInvitation.Accept();
            }
        }

        this.AddEvent(NewInvitationCreated.Create(email, Organizer.Name, ExplicitMeetingId, code));
        IncrementVersion();

        return invitation;
    }

    public void AcceptInvitation(InvitationCode code)
    {
        this.CheckIfMeetingOperationIsPossible();
        this.CheckIfMeetingWasCanceled();

        var invitation = FindInvitationByCode(code);

        invitation.Accept();

        AcceptedInvitations = NumberInvitationsByStatus(InvitationStatus.Accepted);

        this.AddEvent(InvitationAccepted.Create(ExplicitMeetingId.ToString(), Organizer.Identifier, invitation.RecipientName));
        IncrementVersion();
    }

    public void RejectInvitation(InvitationCode code)
    {
        this.CheckIfMeetingOperationIsPossible();
        this.CheckIfMeetingWasCanceled();

        var invitation = FindInvitationByCode(code);
        invitation.Reject();

        AcceptedInvitations = NumberInvitationsByStatus(InvitationStatus.Accepted);

        this.AddEvent(InvitationRejected.Create(ExplicitMeetingId.ToString(), Organizer.Identifier, invitation.RecipientName));
        IncrementVersion();
    }

    public void ExpireInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();

        var invitation = FindInvitationByEmail(email);
        invitation.Expire();

        this.AddEvent(InvitationExpired.Create(email, Organizer.Name));
        IncrementVersion();
    }

    /// <summary>
    /// Currently this method runs every hour using quartz
    /// </summary>
    /// <exception cref="MeetingActiveNowException"></exception>
    public void Complete()
    {
        this.CheckIfMeetingWasCanceled();

        if (Status == MeetingStatus.Active && Date.StartDate.Date >= Clock.CurrentDate().Date)
        {
            throw new MeetingActiveNowException(this.Id);
        }

        Status = MeetingStatus.Completed;
        IncrementVersion();
    }

    public void AddComment(string creatorId, string creatorName, Content content)
    {
        var newComment = Comment.CreateComment(this.Id, creatorName, creatorId, content);
        _comments.Add(newComment);
        IncrementVersion();
    }

    public void UpdateComment(string creatorId, int commentId, Content content)
    {
        var comment = _comments.FirstOrDefault(_ => _.Id == commentId && _.CreatorId == creatorId);
        if (comment == null)
        {
            throw new CommentNotFoundException(creatorId, this.Id);
        }

        comment.Update(content);
        IncrementVersion();
    }

    public void DeleteComment(string creatorId, int commentId)
    {
        var comment = _comments.FirstOrDefault(_ => _.Id == commentId && _.CreatorId == creatorId);
        if (comment == null)
        {
            throw new CommentNotFoundException(creatorId, this.Id);
        }

        _comments.Remove(comment);
        IncrementVersion();
    }

    /// <summary>
    /// Method responsible for changing the status of the invitation request
    /// Method available only to the meeting creator
    /// </summary>
    /// <param name="requestInvitationCreatorId"></param>
    /// <param name="reason"></param>
    public void RejectRequestInvitation(string requestInvitationCreatorId, string? reason = null)
    {
        var requestInvitation = this._requests.FirstOrDefault(_ => _.RequestCreator.Identifier == requestInvitationCreatorId);
        if (requestInvitation == null)
        {
            throw new InvitationRequestNotFoundException(this.Id, requestInvitationCreatorId);
        }

        requestInvitation.Reject(reason);
    }

    /// <summary>
    /// Method available to invitation request creator.
    /// E.g. I've created an invitation request by mistake and I want to delete it
    /// </summary>
    /// <param name="requestInvitationCreatorId"></param>
    public void DeleteRequestInvitation(string requestInvitationCreatorId)
    {
        var requestInvitation = this._requests.FirstOrDefault(_ => _.RequestCreator.Identifier == requestInvitationCreatorId);
        if (requestInvitation == null)
        {
            throw new InvitationRequestNotFoundException(this.Id, requestInvitationCreatorId);
        }

        this._requests.Remove(requestInvitation);
    }

    private void CheckIfMeetingOperationIsPossible()
    {
        if (Date.StartDate.Date <= Clock.CurrentDate().Date)
        {
            throw new MeetingEndedException(this.Id);
        }
    }

    private void CheckIfMeetingWasCanceled()
    {
        if (Status == MeetingStatus.Cancelled)
        {
            throw new MeetingAlreadyCanceledException(this.Id);
        }
    }

    private Invitation FindInvitationByEmail(string email) =>
        _invitations.SingleOrDefault(_ => _.Email == email) ?? throw new InvitationNotFoundException(email, Id);

    private Invitation FindInvitationByCode(string code) =>
        _invitations.SingleOrDefault(_ => _.Code == code) ?? throw new InvitationNotFoundException(code, Id);
}