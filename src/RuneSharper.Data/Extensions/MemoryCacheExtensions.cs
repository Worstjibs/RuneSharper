using Microsoft.Extensions.Caching.Memory;

namespace RuneSharper.Data.Extensions;

public static class MemoryCacheExtensions
{
    public static async Task<IEnumerable<T>> GetOrCreateAsync<T, TKey>(
        this IMemoryCache memoryCache,
        IEnumerable<TKey> entityKeys,
        Func<TKey, string> cacheKeySelector,
        Func<IEnumerable<TKey>, Task<IEnumerable<T>>> entityAsyncDelegate,
        Func<T, TKey> entityKeySelector,
        TimeSpan absoluteExpirationRelativeToNow) where TKey : notnull
    {
        var cachedEntities = entityKeys.ToDictionary(x => x, x => memoryCache.Get<T>(cacheKeySelector(x)));

        var nonCachedEntityKeys = cachedEntities.Where(x => x.Value is null).Select(x => x.Key).ToList();

        if (!nonCachedEntityKeys.Any())
            return cachedEntities.Select(x => x.Value).ToList()!;

        var newEntities = await entityAsyncDelegate(nonCachedEntityKeys);

        foreach (var newEntity in newEntities)
        {
            memoryCache.Set(cacheKeySelector(entityKeySelector(newEntity)), newEntity, absoluteExpirationRelativeToNow);
        }

        return cachedEntities.Where(x => x.Value is not null).Select(x => x.Value)!.Union(newEntities).ToList();
    }
}
