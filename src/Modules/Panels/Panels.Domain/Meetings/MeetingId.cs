using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings;

//This will be changed to https://stackoverflow.com/questions/211498/is-there-a-net-equivalent-to-sql-servers-newsequentialid
public class MeetingId
{
    public Guid Value { get; }

    public MeetingId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static MeetingId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(MeetingId date)
        => date.Value;

    public static implicit operator MeetingId(Guid value)
        => new(value);

    public override string ToString() => Value.ToString("N");
}