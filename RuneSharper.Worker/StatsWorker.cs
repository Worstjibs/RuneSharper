using Microsoft.Extensions.Options;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Settings;

namespace RuneSharper.Worker;

public class StatsWorker : BaseTimedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RuneSharperSettings _settings;

    public StatsWorker(
        ILogger<StatsWorker> logger,
        IServiceProvider serviceProvider,
        IOptions<RuneSharperSettings> options) : base(logger)
    {
        _serviceProvider = serviceProvider;
        _settings = options.Value;
    }

    protected override int Interval => _settings.OsrsApiPollingTime;

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
            return;

        _logger.LogInformation("{Timestamp}: Fetching stats for configured players", DateTimeOffset.Now);

        using var scope = _serviceProvider.CreateScope();

        var saveStatsService = scope.ServiceProvider.GetRequiredService<ISaveStatsService>();

        await saveStatsService.SaveStatsForCharacters(_settings.CharacterNames);

        _logger.LogInformation("{Timestamp}: Stats fetched for users", DateTimeOffset.Now);
    }
}