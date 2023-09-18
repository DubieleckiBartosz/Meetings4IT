using Meetings4IT.Shared.Implementations.Decorators;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Meetings.Events;
using Panels.Domain.ScheduledMeetings;
using Panels.Domain.ScheduledMeetings.ValueObjects;
using Serilog;

namespace Panels.Application.EventHandlers;

public class MeetingCreatedEventHandler : IDomainEventHandler<MeetingCreated>
{
    private readonly IScheduledMeetingRepository _scheduledMeetingRepository;
    private readonly ILogger _logger;

    public MeetingCreatedEventHandler(
        IScheduledMeetingRepository scheduledMeetingRepository,
        ILogger logger)
    {
        _scheduledMeetingRepository = scheduledMeetingRepository;
        _logger = logger;
    }

    public async Task Handle(DomainEvent<MeetingCreated> notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var @event = notification.Event;
        var meetingCreatorId = @event.MeetingCreator.Identifier;
        var schedule = await _scheduledMeetingRepository.GetScheduledMeetingAsync(meetingCreatorId, cancellationToken);
        var upcomingMeeting = UpcomingMeeting.Create(@event.MeetingId, @event.Date);

        if (schedule == null)
        {
            _logger.Information($"Schedule doesn't exist for user {meetingCreatorId}.");

            schedule = ScheduledMeeting.CreateMeetingSchedule(@event.MeetingCreator);
            schedule.NewUpcomingMeeting(upcomingMeeting);

            await _scheduledMeetingRepository.CreateScheduledMeetingAsync(schedule, cancellationToken);

            _logger.Information($"Schedule has been created for user {meetingCreatorId}.");

            return;
        }

        schedule.NewUpcomingMeeting(upcomingMeeting);

        _scheduledMeetingRepository.UpdateScheduledMeeting(schedule, cancellationToken);

        _logger.Information($"Upcoming meeting has been added to schedule.");
    }
}