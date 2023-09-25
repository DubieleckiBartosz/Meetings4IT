using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Panels.Application.Options;
using System.Diagnostics;
using ILogger = Serilog.ILogger;

namespace Panels.Infrastructure.Outbox;

internal sealed class OutboxPanelProcessor : BackgroundService
{
    private int _isProcessing;
    private readonly TimeSpan _startDelay;
    private readonly TimeSpan _interval;
    private readonly bool _enabled;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger _logger;

    public OutboxPanelProcessor(IServiceScopeFactory serviceScopeFactory, ILogger logger, IOptions<OutboxPanelOptions> options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;

        var panelOutboxOptions = options.Value;
        _enabled = panelOutboxOptions.Enabled;
        _interval = panelOutboxOptions.Interval ?? TimeSpan.FromSeconds(2);
        _startDelay = panelOutboxOptions.StartDelay ?? TimeSpan.FromSeconds(5);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_enabled)
        {
            _logger.Warning("OutboxPanel is disabled");
            return;
        }

        _logger.Information($"OutboxPanel is enabled. Start delay: {_startDelay}, interval: {_interval}");
        await Task.Delay(_startDelay, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (Interlocked.Exchange(ref _isProcessing, 1) == 1)
            {
                await Task.Delay(_interval, stoppingToken);
                continue;
            }

            _logger.Information("Started processing OutboxPanel messages...");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var outbox = scope.ServiceProvider.GetRequiredService<IOutboxPanelAccessor>();
                    await outbox!.PublishUnsentAsync(stoppingToken);
                }
                catch (Exception exception)
                {
                    _logger.Error("There was an error when processing PanelOutbox.");
                    _logger.Error(exception, exception.Message);
                }
                finally
                {
                    Interlocked.Exchange(ref _isProcessing, 0);
                    stopwatch.Stop();
                    _logger.Information($"Finished processing PanelOutbox messages in {stopwatch.ElapsedMilliseconds} ms.");
                }
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}