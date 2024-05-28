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

    public string Add<T>(string key, T value)
    {
        string jsonValue = JsonSerializer.Serialize(value);
        StoreKey(key);

        return _memoryCache.Set(key, jsonValue);
    }

    public string AddRange<T>(string key, IEnumerable<T> values)
    {
        var jsonValues = JsonSerializer.Serialize(values);
        StoreKey(key);

        return _memoryCache.Set(key, jsonValues);
    }

    public void Delete(string key)
    {
        _keys.Remove(key);
        _memoryCache.Remove(key);
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
            Delete(key);
        }
    }

    public T Get<T>(string key)
    {
        var value = _memoryCache.Get(key);

        return JsonSerializer.Deserialize<T>(value?.ToString());
    }

    public IEnumerable<string> GetKeys(string? pattern)
    {
        return _keys;
    }

    private void StoreKey(string key)
    {
        _keys.Add(key);
    }

    public bool IsExists(string key)
    {
        return _memoryCache.TryGetValue(key, out object? _);
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire)
    {
        var task = _memoryCache.GetOrCreate(
           key,
           entry => new Lazy<Task<T>>(acquire, true));

        try
        {
            var result = await task!.Value;

            if (!IsExists(key))
                StoreKey(key);

            return result;
        }
        catch
        {
            Delete(key);

            throw;
        }
    }
}