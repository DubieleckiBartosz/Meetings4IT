using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Generators;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.Entities;
using Panels.Domain.Meetings.Events;
using Panels.Domain.Meetings.Exceptions;
using Panels.Domain.Meetings.Exceptions.InvitationExceptions;
using Panels.Domain.Meetings.Statuses;
using Panels.Domain.Meetings.ValueObjects;

namespace Panels.Domain.Meetings;

public class Meeting : Entity, IAggregateRoot
{
    private readonly List<Invitation> _invitations;
    private readonly List<MeetingImage> _images;

    /// <summary>
    /// This property is responsible for upcoming meetings.
    /// We don't have access to the int Id when creating a meeting
    /// </summary>
    public MeetingId ExplicitMeetingId { get; }

    public UserInfo Organizer { get; }
    public int CategoryIndex { get; private set; }
    public MeetingCategory Category { get; private set; }

    //Anyone can come if public property is true
    public bool IsPublic { get; private set; }

    public int? MaxInvitations { get; private set; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public MeetingCancellation? Cancellation { get; private set; }
    public DateRange Date { get; private set; }

    public bool Completed => Date.StartDate <= Clock.CurrentDate() && Cancellation == null;
    private int AcceptedInvitations => NumberInvitationsByStatus(InvitationStatus.Accepted);
    private int PendingInvitations => NumberInvitationsByStatus(InvitationStatus.Pending);

    private int NumberInvitationsByStatus(InvitationStatus status)
    {
        return _invitations.Count(_ => _.Status == status);
    }

    private Meeting()
    {
        this._invitations = new();
        this._images = new();
    }

    private Meeting(
        UserInfo organizer,
        MeetingCategory category,
        Description description,
        Address address,
        DateRange date,
        bool isPublic,
        int? maxInvitations)
    {
        Organizer = organizer;
        CategoryIndex = category.Index;
        Description = description;
        Address = address;
        Date = date;
        IsPublic = isPublic;
        MaxInvitations = maxInvitations;
        ExplicitMeetingId = MeetingId.Create();
        this._invitations = new();
        this._images = new();

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

        var invitations = _invitations
            .Where(_ => _.Status == InvitationStatus.Pending || _.Status == InvitationStatus.Accepted).ToList();

        foreach (var invitation in invitations)
        {
            invitation.Cancel();
        }

        var invitationRecipients = invitations.Select(_ => _.Email.Value).ToList();

        this.AddEvent(MeetingCanceled.Create(Id, invitationRecipients));
        IncrementVersion();
    }

    public Invitation CreateNewInvitation(Email email, Date invitationExpirationDate)
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

        var invitation = new Invitation(email, invitationExpirationDate, code);
        _invitations.Add(invitation);

        this.AddEvent(NewInvitationCreated.Create(email, Organizer.Name, ExplicitMeetingId));
        IncrementVersion();

        return invitation;
    }

    public void AcceptInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();
        this.CheckIfMeetingWasCanceled();

        var invitation = FindInvitation(email);

        invitation.Accept();

        this.AddEvent(InvitationAccepted.Create(Id, Organizer.Identifier, email));
        IncrementVersion();
    }

    public void RejectInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();

        var invitation = FindInvitation(email);
        invitation.Reject();

        this.AddEvent(InvitationRejected.Create(Organizer.Identifier));
        IncrementVersion();
    }

    public void ExpireInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();

        var invitation = FindInvitation(email);
        invitation.Expire();

        this.AddEvent(InvitationExpired.Create(email, Organizer.Name));
        IncrementVersion();
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
        if (Cancellation != null)
        {
            throw new MeetingAlreadyCanceledException(this.Id);
        }
    }

    private Invitation FindInvitation(string email) =>
        _invitations.SingleOrDefault(_ => _.Email == email) ?? throw new InvitationNotFoundException(email, Id);
}