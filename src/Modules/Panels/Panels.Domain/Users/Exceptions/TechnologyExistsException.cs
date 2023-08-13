using Meetings4IT.Shared.Abstractions.Exceptions; 

namespace Panels.Domain.Users.Exceptions;

public class TechnologyExistsException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public TechnologyExistsException() : base("Technology is already assigned to the user.")
    {
    }
}