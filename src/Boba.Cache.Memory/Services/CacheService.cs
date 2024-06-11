using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Boba.Cache.Memory;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly HashSet<string> _keys;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _keys = new();
    }

    public Task AddAsync<T>(string key, T value)
    {
        string jsonValue = JsonSerializer.Serialize(value);
        StoreKey(key);

        _memoryCache.Set(key, jsonValue);

        return Task.CompletedTask;
    }

    public Task AddRangeAsync<T>(string key, IEnumerable<T> values)
    {
        var jsonValues = JsonSerializer.Serialize(values);
        StoreKey(key);

        _memoryCache.Set(key, jsonValues);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string key)
    {
        _keys.Remove(key);
        _memoryCache.Remove(key);

        return Task.CompletedTask;
    }

    public void DeleteAll()
    {
        foreach (var entry in _keys)
        {
            _memoryCache.Remove(entry);
        }

        _keys.Clear();
    }

    public void DeleteAllWithPrefix(string prefix)
    {
        var keysWithPrefix = GetKeys(null).Where(k => k.StartsWith(prefix));

        foreach (var key in keysWithPrefix)
        {
            DeleteAsync(key);
        }
    }

    public Task<T> GetAsync<T>(string key)
    {
        var value = _memoryCache.Get(key);

        return Task.FromResult(JsonSerializer.Deserialize<T>(value?.ToString()));
    }

    public IEnumerable<string> GetKeys(string? pattern)
    {
        return _keys;
    }

    private void StoreKey(string key)
    {
        _keys.Add(key);
    }

    public Task<bool> IsExistsAsync(string key)
    {
        return Task.FromResult(_memoryCache.TryGetValue(key, out object? _));
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire)
    {
        var task = _memoryCache.GetOrCreate(
           key,
           entry => new Lazy<Task<T>>(acquire, true));

        try
        {
            var result = await task!.Value;

            if (!await IsExistsAsync(key))
                StoreKey(key);

            return result;
        }
        catch
        {
            DeleteAsync(key);

            throw;
        }
    }
}