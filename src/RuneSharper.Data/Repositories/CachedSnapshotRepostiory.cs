using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
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
        return await GetOrCreateFromCache(userNames);
    }

    private async Task<IEnumerable<Snapshot>> GetOrCreateFromCache(IEnumerable<string> userNames)
    {
        Func<string, string> keySelector = x => $"snapshot-latest-{x}";

        var cachedSnapshots = userNames.ToDictionary(x => x, x => _memoryCache.Get<Snapshot>(keySelector(x)));

        var nonCachedUserNames = cachedSnapshots.Where(x => x.Value is null).Select(x => x.Key).ToList();

        if (!nonCachedUserNames.Any())
            return cachedSnapshots.Select(x => x.Value).ToList()!;

        var newSnapshots = await _snapshotRepository.GetLatestSnapshotsAsync(nonCachedUserNames);

        foreach (var snapshot in newSnapshots)
        {
            _memoryCache.Set(keySelector(snapshot.Character.UserName), snapshot);
        }

        return cachedSnapshots.Where(x => x.Value is not null).Select(x => x.Value).Union(newSnapshots).ToList()!;
    }
}
