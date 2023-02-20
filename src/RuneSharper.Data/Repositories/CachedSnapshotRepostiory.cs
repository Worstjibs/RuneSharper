using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RuneSharper.Data.Extensions;
using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Domain.Helpers;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Shared.Extensions;
using RuneSharper.Application.Settings;

namespace RuneSharper.Data.Repositories;

public class CachedSnapshotRepostiory : Repository<Snapshot>, ISnapshotRepository
{
    private readonly SnapshotRepository _snapshotRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<RuneSharperSettings> _settings;

    private const double _nearestMinute = 15;

    public CachedSnapshotRepostiory(
        RuneSharperContext context,
        SnapshotRepository snapshotRepository,
        IMemoryCache memoryCache,
        IOptions<RuneSharperSettings> settings) 
        : base(context)
    {
        _snapshotRepository = snapshotRepository;
        _memoryCache = memoryCache;
        _settings = settings;
    }

    public async Task<Snapshot?> GetFirstSnapshotAsync(string userName, DateRange dateRange)
    {
        var dateFromRounded = dateRange.DateFrom.RoundDown(TimeSpan.FromMinutes(_nearestMinute));

        var diff = dateRange.DateFrom.RoundUp(TimeSpan.FromMinutes(_nearestMinute)) - dateRange.DateFrom;

        var key = $"snapshot-first-{userName}" +
            $"-{dateFromRounded:yyyyMMddHHmm}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(diff);

                return _snapshotRepository.GetFirstSnapshotAsync(userName, dateRange);
            });
    }

    public async Task<Snapshot?> GetLastSnapshotAsync(string userName, DateRange dateRange)
    {
        var dateToRounded = dateRange.DateTo.RoundDown(TimeSpan.FromMinutes(_nearestMinute));

        var diff = dateRange.DateTo.RoundUp(TimeSpan.FromMinutes(_nearestMinute)) - dateRange.DateFrom;

        var key = $"snapshot-last-{userName}" +
            $"-{dateToRounded:yyyyMMddHHmm}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(diff);

                return _snapshotRepository.GetLastSnapshotAsync(userName, dateRange);
            });
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
