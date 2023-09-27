using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;

namespace Panels.Domain.Meetings.Entities;

/// <summary>
/// We actually split user opinions and meeting comments
/// into two separate tables, but that's for optimization purposes.
/// User opinions are more extensive and doing a join every time may lead to a longer wait for the response.
/// A deleted comment is not stored!
/// </summary>
public class Comment : Entity
{
    public int MeetingId { get; }

    /// <summary>
    /// CreatorId for quick search
    /// </summary>
    public string CreatorId { get; }

    public string CreatorName { get; }
    public Content Content { get; private set; }
    public DateTime Created { get; }
    public DateTime Modified { get; private set; }

    private Comment(int meetingId, string creatorName, string creatorId, Content content)
    {
        MeetingId = meetingId;
        CreatorName = creatorName;
        Content = content;
        Created = Clock.CurrentDate();
        Modified = Clock.CurrentDate();
        CreatorId = creatorId;
    }

    public static Comment CreateComment(int meetingId, string creatorName, string creatorId, Content content)
    {
        return new Comment(meetingId, creatorName, creatorId, content);
    }

    public void Update(Content newContent) => (Content, Modified) = (newContent, Clock.CurrentDate());
}