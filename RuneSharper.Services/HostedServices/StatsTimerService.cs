using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RuneSharper.Data;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Services.HostedServices {
    public sealed class StatsTimerService : IHostedService {
        private readonly ILogger<StatsTimerService> _logger;
        private readonly IStatsService _statsService;
        private readonly IServiceProvider _services;
        private Timer? _timer;

        private readonly string[] ACCOUNT_NAMES = { "Worstjibs" };

        public StatsTimerService(ILogger<StatsTimerService> logger, IStatsService statsService, IServiceProvider services) {
            _logger = logger;
            _statsService = statsService;
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
                    var account = context.Accounts.Include(x => x.Snapshots).FirstOrDefault(x => x.UserName == accountName);

                    if (account is null) {
                        account = new Account {
                            UserName = accountName,
                            Snapshots = new List<Snapshot>()
                        };

                        context.Accounts.Add(account);
                    }

                    var skills = _statsService.QueryStatsForUserId(accountName);

                    account.Snapshots.Add(new Snapshot {
                        Account = account,
                        Skills = skills.Select(x => new SkillSnapshot {
                            Experience = x.Experience,
                            Level = x.Level,
                            Rank = x.Rank
                        }).ToList()
                    });

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
