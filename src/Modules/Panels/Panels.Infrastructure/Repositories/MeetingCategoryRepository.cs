using Microsoft.EntityFrameworkCore;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Meetings.Categories;
using Panels.Infrastructure.Database;

namespace Panels.Infrastructure.Repositories;

public class MeetingCategoryRepository : IMeetingCategoryRepository
{
    private readonly DbSet<MeetingCategory> _categories;

    public MeetingCategoryRepository(
        PanelContext context)
    {
        _categories = context.MeetingCategories;
    }

    public async Task<string?> GetMeetingCategoryByIndexAsync(int index, CancellationToken cancellationToken = default)
    {
        var category = await _categories.AsNoTracking().FirstOrDefaultAsync(_ => _.Index == index);
        return category?.Value;
    }
}