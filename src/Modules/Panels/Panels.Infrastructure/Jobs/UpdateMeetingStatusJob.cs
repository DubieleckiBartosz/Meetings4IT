using Panels.Application.Contracts.Repositories;
using Quartz;
using Serilog;

namespace Panels.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateMeetingStatusJob : IJob
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ILogger _logger;

    public UpdateMeetingStatusJob(IMeetingRepository meetingRepository, ILogger logger)
    {
        _meetingRepository = meetingRepository;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var logId = Guid.NewGuid();

        _logger.Information($"--------Start job MeetingStatusAsCompletedJob ${logId}--------");

        var meetings = await _meetingRepository.GetAllCompletedMeetingsForStatusUpdatesAsync(context.CancellationToken);
        if (meetings == null || !meetings.Any())
        {
            _logger.Information($"There is no meeting to complete ${logId}");
            return;
        }

        foreach (var meeting in meetings)
        {
            meeting.Complete();
            _meetingRepository.UpdateMeeting(meeting);
            _logger.Information($"Meeting {meeting.Id} set as completed ${logId}");
        }

        await _meetingRepository.UnitOfWork.SaveChangesAsync(context.CancellationToken);

        _logger.Information($"Changes saved ${logId}");
    }
}