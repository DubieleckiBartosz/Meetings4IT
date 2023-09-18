using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Meetings;
using Panels.Infrastructure.Database;
using Polly;
using Serilog;
using static Dapper.SqlMapper;

namespace Panels.Infrastructure.Repositories;

public class MeetingRepository : DapperContext, IMeetingRepository
{
    private readonly DbSet<Meeting> _meetings;
    private readonly PanelContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public MeetingRepository(
        PanelContext context,
        IOptions<DapperOptions> options,
        ILogger logger) : base(options.Value?.DefaultConnection!, logger)
    {
        _meetings = context.Meetings;
        _context = context;
    }

    public async Task CreateNewMeetingAsync(Meeting meeting, CancellationToken cancellationToken = default)
    {
        await _meetings.AddAsync(meeting, cancellationToken);
    }

    public Task<Meeting> GetMeetingByIdAsync(int meetingId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}