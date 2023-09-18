using Meetings4IT.Shared.Abstractions.Kernel;
using Microsoft.EntityFrameworkCore;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.ScheduledMeetings;
using Panels.Infrastructure.Database;

namespace Panels.Infrastructure.Repositories;

public class ScheduledMeetingRepository : IScheduledMeetingRepository
{
    private readonly DbSet<ScheduledMeeting> _scheduledMeetings;
    private readonly PanelContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public ScheduledMeetingRepository(PanelContext context)
    {
        _scheduledMeetings = context.ScheduledMeetings;
        _context = context;
    }

    public async Task<ScheduledMeeting?> GetScheduledMeetingAsync(string owner, CancellationToken cancellationToken = default)
    {
        var scheduledMeeting = await _scheduledMeetings.SingleOrDefaultAsync(_ => _.ScheduleOwner.Identifier == owner);
        return scheduledMeeting;
    }

    public async Task CreateScheduledMeetingAsync(ScheduledMeeting scheduledMeeting, CancellationToken cancellationToken = default)
    {
        await _scheduledMeetings.AddAsync(scheduledMeeting);
    }

    public void UpdateScheduledMeeting(ScheduledMeeting scheduledMeeting, CancellationToken cancellationToken = default)
    {
        _scheduledMeetings.Update(scheduledMeeting);
    }
}