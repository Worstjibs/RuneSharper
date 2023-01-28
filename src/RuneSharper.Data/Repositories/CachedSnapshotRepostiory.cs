using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RuneSharper.Data.Extensions;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Helpers;
using RuneSharper.Shared.Settings;

namespace RuneSharper.Data.Repositories;

public class CachedSnapshotRepostiory : Repository<Snapshot>, ISnapshotRepository
{
    private readonly SnapshotRepository _snapshotRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<RuneSharperSettings> _settings;

    public CachedSnapshotRepostiory(
        RuneSharperContext context,
        SnapshotRepository snapshotRepository,
        IMemoryCache memoryCache,
        IOptions<RuneSharperSettings> settings) : base(context)
    {
        _snapshotRepository = snapshotRepository;
        _memoryCache = memoryCache;
        _settings = settings;
    }

    public async Task<(Snapshot?, Snapshot?)> GetFirstAndLastSnapshots(string userName, DateRange dateRange)
    {
        return await _snapshotRepository.GetFirstAndLastSnapshots(userName, dateRange);
    }

    public async Task<Snapshot?> GetLatestSnapshotAsync(string userName)
    {
        string key = $"snapshot-latest-{userName}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(_settings.Value.OsrsApiPollingTime));

                return _snapshotRepository.GetLatestSnapshotAsync(userName);
            });
    }

    public async Task<IEnumerable<Snapshot>> GetLatestSnapshotsAsync(IEnumerable<string> userNames)
    {
        return await _memoryCache.GetOrCreateAsync(
            userNames,
            x => $"snapshot-latest-{x}",
            _snapshotRepository.GetLatestSnapshotsAsync,
            x => x.Character.UserName,
            TimeSpan.FromSeconds(_settings.Value.OsrsApiPollingTime));
    }
}
