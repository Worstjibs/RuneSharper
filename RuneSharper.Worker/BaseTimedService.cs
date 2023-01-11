using RuneSharper.Shared.Extensions;
using System.Diagnostics;

namespace RuneSharper.Worker;

public abstract class BaseTimedService : BackgroundService
{
    private PeriodicTimer? _timer;
    protected readonly ILogger<BaseTimedService> _logger;

    protected abstract int Interval { get; }

    protected BaseTimedService(ILogger<BaseTimedService> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(Interval));

        _logger.LogInformation("Configured time interval for timed service is: {TimedServiceInterval}", Interval);

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            try
            {
                _logger.LogInformation("Beginning Timed service {TimedServiceName}", GetType().Name);
                var startTime = Stopwatch.GetTimestamp();

                await DoWorkAsync(stoppingToken);

                var elapsedTime = Stopwatch.GetElapsedTime(startTime);
                _logger.LogInformation("Timed service {TimedServiceName} finished. Time taken {ElapsedTime}ms", GetType().Name, elapsedTime.Milliseconds);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Timed Service run failed.");
            }
        } while (await _timer!.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested);
    }

    protected abstract Task DoWorkAsync(CancellationToken stoppingToken);
}