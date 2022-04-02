using Microsoft.Extensions.Options;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Settings;

namespace RuneShaper.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly RuneSharperSettings _settings;

        private readonly string[] ACCOUNT_NAMES = { "worstjibs" };

        public Worker(
            ILogger<Worker> logger,
            IServiceProvider serviceProvider,
            IOptions<RuneSharperSettings> options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetching stats for configured players", DateTimeOffset.Now);

                using var scope = _serviceProvider.CreateScope();

                var saveStatsService = scope.ServiceProvider.GetService<ISaveStatsService>();

                if (saveStatsService == null)
                {
                    throw new ArgumentNullException("Save Stats Service is null, review DI Configuration");
                }

                await saveStatsService.SaveStatsForUsers(ACCOUNT_NAMES);

                await Task.Delay(_settings.OsrsApiPollingTime * 1000, stoppingToken);
            }
        }
    }
}