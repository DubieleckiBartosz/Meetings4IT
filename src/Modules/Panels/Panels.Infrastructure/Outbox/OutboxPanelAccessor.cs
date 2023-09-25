using Meetings4IT.Shared.Abstractions.Time;
using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Meetings4IT.Shared.Implementations.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Panels.Application.Options;
using Panels.Infrastructure.Database;
using Serilog;

namespace Panels.Infrastructure.Outbox;

public class OutboxPanelAccessor : IOutboxPanelAccessor
{
    private readonly bool _enabled;
    private readonly PanelContext _context;
    private readonly DbSet<OutboxMessage> _messages;
    private readonly ILogger _logger;
    private readonly IEventBus _eventBus;
    private readonly OutboxPanelOptions _options;

    public OutboxPanelAccessor(
        PanelContext panelContext,
        ILogger logger,
        IOptions<OutboxPanelOptions> options,
        IEventBus eventBus)
    {
        _context = panelContext;
        _messages = _context.OutboxMessages;
        _logger = logger;
        _eventBus = eventBus;
        _options = options.Value;
        _enabled = _options.Enabled;
    }

    public async Task AddAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IntegrationEvent
    {
        var type = @event?.GetType()?.AssemblyQualifiedName;

        if (type != null)
        {
            var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            var outboxMessage = new OutboxMessage(@event!.CreationDate, type, data);

            await _context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        }
        else
        {
            _logger.Error("Event is null.");
        }
    }

    public Task SaveAsync()
    {
        // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
        return Task.CompletedTask;
    }

    public async Task PublishUnsentAsync(CancellationToken cancellationToken = default)
    {
        if (!_enabled)
        {
            _logger.Warning($"PanelOutbox is disabled, outgoing messages won't be sent.");
            return;
        }

        var unsentMessages = await _messages.Where(x => x.ProcessedDate == null).ToListAsync();
        if (!unsentMessages.Any())
        {
            _logger.Warning($"No unsent messages found in PanelOutbox.");
            return;
        }

        _logger.Information($"Found {unsentMessages.Count} unsent messages in PanelOutbox, sending...");
        foreach (var outboxMessage in unsentMessages)
        {
            var type = Type.GetType(outboxMessage.Type);
            var message = JsonConvert.DeserializeObject(outboxMessage.Data, type) as IntegrationEvent;
            if (message is null)
            {
                _logger.Error($"Invalid message type in PanelOutbox: '{type.Name}', Identifier: '{outboxMessage.Id}");
                continue;
            }

            outboxMessage.ProcessedDate = Clock.CurrentDate();

            _logger.Information($"Publishing a message from PanelOutbox {outboxMessage.Id}");

            await _eventBus.PublishAsync(cancellationToken, message);

            _messages.Update(outboxMessage);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}