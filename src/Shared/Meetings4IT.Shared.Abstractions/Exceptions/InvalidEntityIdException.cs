namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class InvalidEntityIdException : BaseException
{
    public object Id { get; }

    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
        => Id = id;
}