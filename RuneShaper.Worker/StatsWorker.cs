using Microsoft.Extensions.Options;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Settings;

namespace RuneShaper.Worker
{
    public class StatsWorker : BackgroundService
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && _settings.OsrsApiPollingTime > 0)
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

                await Task.Delay(_settings.OsrsApiPollingTime * 1000, stoppingToken);
            }
        }
    }
}