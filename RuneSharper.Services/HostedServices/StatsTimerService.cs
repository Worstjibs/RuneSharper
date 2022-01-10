using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RuneSharper.Data;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.HostedServices {
    public sealed class StatsTimerService : IHostedService {
        private readonly ILogger<StatsTimerService> _logger;
        private readonly IOsrsApiService _osrsApiService;
        private readonly IServiceProvider _services;
        private Timer? _timer;

        private readonly string[] ACCOUNT_NAMES = { "worstjibs" };

        public StatsTimerService(ILogger<StatsTimerService> logger, IOsrsApiService osrsApiService, IServiceProvider services) {
            _logger = logger;
            _osrsApiService = osrsApiService;
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("{Service} is running", nameof(StatsTimerService));

            _timer = new Timer(FetchStats, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

            return Task.CompletedTask;
        }

        public async void FetchStats(object? state) {
            using var scope = _services.CreateScope();

            var context = scope.ServiceProvider.GetService<RuneSharperContext>();

            if (context is not null) {
                foreach (var accountName in ACCOUNT_NAMES) {
                    var account = context.Characters.Include(x => x.Snapshots).FirstOrDefault(x => x.UserName == accountName.ToLower());

                    if (account is null) {
                        account = new Character {
                            UserName = accountName.ToLower(),
                            Snapshots = new List<Snapshot>()
                        };

                        context.Characters.Add(account);
                    }

                    account.Snapshots.Add(_osrsApiService.QueryHiScoresByAccount(account));

                    await context.SaveChangesAsync();
                }
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("{Service} is running", nameof(StatsTimerService));

            return Task.CompletedTask;
        }
    }
}
