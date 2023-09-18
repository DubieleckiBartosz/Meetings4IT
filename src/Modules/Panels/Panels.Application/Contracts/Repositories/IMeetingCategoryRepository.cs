namespace Panels.Application.Contracts.Repositories;

public interface IMeetingCategoryRepository
{
    Task<string?> GetMeetingCategoryByIndexAsync(int index, CancellationToken cancellationToken = default);
}