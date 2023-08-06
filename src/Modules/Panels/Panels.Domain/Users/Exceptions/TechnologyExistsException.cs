using Meetings4IT.Shared.Domain.DomainExceptions;

namespace Panels.Domain.Users.Exceptions;

public class TechnologyExistsException : BusinessException
{
    public TechnologyExistsException() : base("Technology is already assigned to the user.")
    {
    }
}