namespace Boba.Cache;

public class CacheManager(ICacheService cacheService): ICacheManager
{
    public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire)
    {
        var result = await cacheService.GetOrCreateAsync(key, acquire) ?? default!;

        return result;
    }
}
