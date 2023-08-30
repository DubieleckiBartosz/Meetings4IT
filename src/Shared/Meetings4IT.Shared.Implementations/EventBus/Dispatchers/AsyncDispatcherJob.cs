using Meetings4IT.Shared.Implementations.EventBus.Channel;
using Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Meetings4IT.Shared.Implementations.Settings;
using Meetings4IT.Shared.Implementations.Tools;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text;

namespace Meetings4IT.Shared.Implementations.EventBus.Dispatchers;

public class AsyncDispatcherJob : BackgroundService
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IEventChannel _eventChannel;
    private readonly ILogger _logger;
    private readonly IEventRegistry _eventRegistry;

    public AsyncDispatcherJob(IEventDispatcher eventDispatcher, IEventChannel eventChannel, ILogger logger, IEventRegistry eventRegistry)
    {
        _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        _eventChannel = eventChannel ?? throw new ArgumentNullException(nameof(eventChannel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _eventRegistry = eventRegistry ?? throw new ArgumentNullException(nameof(eventRegistry));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    { 
        await foreach (var message in _eventChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                var body = message.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(body);
                var target = _eventRegistry.Navigate(message.Navigator);
                var @event = messageString.Deserialize<IntegrationEvent>(target, JsonSettings.DefaultSerializerSettings)!;

                await _eventDispatcher.PublishAsync(@event);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
            }
        }
    }
}