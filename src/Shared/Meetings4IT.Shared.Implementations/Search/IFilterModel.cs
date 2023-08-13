using Meetings4IT.Shared.Implementations.Search.SearchParameters;

namespace Meetings4IT.Shared.Implementations.Search;

public interface IFilterModel
{
    SortModelParameters Sort { get; set; }
}