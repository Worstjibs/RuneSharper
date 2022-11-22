namespace RuneSharper.Worker;

public abstract class BaseTimedService : BackgroundService
{
    private PeriodicTimer? _timer;
    protected readonly ILogger _logger;

    protected abstract int Interval { get; }

    protected BaseTimedService(ILogger logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(Interval));

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            try
            {
                await DoWorkAsync(stoppingToken);
            } catch (Exception ex)
            {
                _logger.LogError("Timed Service run failed.", ex);
            }            
        } while (await _timer!.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested);
    }

    protected abstract Task DoWorkAsync(CancellationToken stoppingToken);
}