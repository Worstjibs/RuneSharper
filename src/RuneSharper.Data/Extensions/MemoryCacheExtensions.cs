using Microsoft.Extensions.Caching.Memory;

namespace RuneSharper.Data.Extensions;

public static class MemoryCacheExtensions
{
    public static async Task<IEnumerable<T>> GetOrCreateAsync<T>(
        this IMemoryCache memoryCache,
        IEnumerable<string> entityKeys,
        Func<string, string> cacheKeySelector,
        Func<IEnumerable<string>, Task<IEnumerable<T>>> entityDelegate,
        Func<T, string> entityKeySelector,
        TimeSpan absoluteExpirationRelativeToNow)
    {
        var cachedSnapshots = entityKeys.ToDictionary(x => x, x => memoryCache.Get<T>(cacheKeySelector(x)));

        var nonCachedUserNames = cachedSnapshots.Where(x => x.Value is null).Select(x => x.Key).ToList();

        if (!nonCachedUserNames.Any())
            return cachedSnapshots.Select(x => x.Value).ToList()!;

        var newSnapshots = await entityDelegate(entityKeys);

        foreach (var snapshot in newSnapshots)
        {
            memoryCache.Set(cacheKeySelector(entityKeySelector(snapshot)), snapshot, absoluteExpirationRelativeToNow);
        }

        return cachedSnapshots.Where(x => x.Value is not null).Select(x => x.Value)!.Union(newSnapshots).ToList();
    }
}
