using Microsoft.Extensions.Options;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Settings;
using RuneSharper.Worker;

namespace RuneShaper.Worker;

public class StatsWorker : BaseTimedService
{
    private readonly ILogger<StatsWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly RuneSharperSettings _settings;

    public StatsWorker(
        ILogger<StatsWorker> logger,
        IServiceProvider serviceProvider,
        IOptions<RuneSharperSettings> options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _settings = options.Value;
    }

    protected override int Interval => _settings.OsrsApiPollingTime;

    protected override async Task DoWorkAsync()
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