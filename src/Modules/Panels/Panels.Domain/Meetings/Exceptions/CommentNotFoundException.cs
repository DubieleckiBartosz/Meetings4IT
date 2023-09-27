using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class CommentNotFoundException : BaseException
{
    public CommentNotFoundException(string userId, int meetingId) : base($"User comment not found in meeting: [Meeting {meetingId}], [User {userId}]")
    {
    }
}