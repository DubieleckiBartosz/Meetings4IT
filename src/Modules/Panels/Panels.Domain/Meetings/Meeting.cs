using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
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
    private readonly HashSet<Invitation> _invitations = new();
    private readonly HashSet<MeetingImage> _images = new();
    public Email Creator { get; }
    public MeetingCategory Category { get; set; }

    //Anyone can come if public property is true
    public bool IsPublic { get; set; }
    public int? MaxInvitations { get; set; }
    public Description Description { get; set; }
    public Address Address { get; set; }
    public MeetingCancellation? Cancellation { get; set; }
    public DateRange Date { get; set; }
    public List<Invitation> Invitations => _invitations.ToList();
    public List<MeetingImage> Images => _images.ToList();
    public bool Completed => Date.StartDate <= Clock.CurrentDate() && Cancellation == null;
    private int AcceptedInvitations => NumberInvitationsByStatus(InvitationStatus.Accepted);
    private int PendingInvitations => NumberInvitationsByStatus(InvitationStatus.Pending);

    private int NumberInvitationsByStatus(InvitationStatus status)
    {
        return _invitations.Count(_ => _.Status == status);
    }

    private Meeting(
        Email creator,
        MeetingCategory category,
        Description description,
        Address address,
        DateRange date,
        bool isPublic,
        int? maxInvitations)
    {
        Creator = creator;
        Category = category;
        Description = description;
        Address = address;
        Date = date;
        IsPublic = isPublic;
        MaxInvitations = maxInvitations;
        IncrementVersion();

        this.AddEvent(MeetingCreated.Create(creator));
    }

    public static Meeting Create(
        Email creator,
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
            creator,
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

    public void CreateNewInvitation(Email email, Date invitationExpirationDate)
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

        var invitation = new Invitation(email, invitationExpirationDate);
        _invitations.Add(invitation);

        this.AddEvent(NewInvitationCreated.Create(email, Creator));
        IncrementVersion();
    }

    public void AcceptInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();
        this.CheckIfMeetingWasCanceled();

        var invitation = FindInvitation(email);

        invitation.Accept();

        this.AddEvent(InvitationAccepted.Create(Creator));
        IncrementVersion();
    }

    public void RejectInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();

        var invitation = FindInvitation(email);
        invitation.Reject();

        this.AddEvent(InvitationRejected.Create(Creator));
        IncrementVersion();
    }

    public void ExpireInvitation(Email email)
    {
        this.CheckIfMeetingOperationIsPossible();

        var invitation = FindInvitation(email);
        invitation.Expire();

        this.AddEvent(InvitationExpired.Create(email, Creator));
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