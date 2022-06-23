using Microsoft.Extensions.Options;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Settings;

namespace RuneShaper.Worker;

public class StatsWorker : BackgroundService
{
    private readonly ILogger<StatsWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly RuneSharperSettings _settings;

    private readonly PeriodicTimer _timer;

    public StatsWorker(
        ILogger<StatsWorker> logger,
        IServiceProvider serviceProvider,
        IOptions<RuneSharperSettings> options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _settings = options.Value;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(_settings.OsrsApiPollingTime));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            await DoWorkAsync();
        } while (await _timer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested);
    }

    private async Task DoWorkAsync()
    {
        _logger.LogInformation("Fetching stats for configured players", DateTimeOffset.Now);

        using var scope = _serviceProvider.CreateScope();

        var saveStatsService = scope.ServiceProvider.GetService<ISaveStatsService>();

        if (saveStatsService == null)
        {
            throw new ArgumentNullException("Save Stats Service is null, review DI Configuration");
        }

        await saveStatsService.SaveStatsForCharacters(_settings.CharacterNames);

        _logger.LogInformation("Stats fetched for users", DateTimeOffset.Now);
    }
}