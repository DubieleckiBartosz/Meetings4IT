using Meetings4IT.Shared.Abstractions.Kernel; 

namespace Panels.Domain.Meetings.Statuses;

public class InvitationStatus : Enumeration
{
    public static InvitationStatus Pending = new InvitationStatus(1, nameof(Pending));
    public static InvitationStatus Accepted = new InvitationStatus(2, nameof(Accepted));
    public static InvitationStatus Rejected = new InvitationStatus(3, nameof(Rejected));
    public static InvitationStatus Expired = new InvitationStatus(4, nameof(Expired));
    public static InvitationStatus Canceled = new InvitationStatus(5, nameof(Canceled));
    protected InvitationStatus(int id, string name) : base(id, name)
    {
    }
}