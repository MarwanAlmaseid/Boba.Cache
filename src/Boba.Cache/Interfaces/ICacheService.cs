namespace Boba.Cache;

public interface ICacheService
{
    Task AddAsync<T>(string key, T value);
    Task AddRangeAsync<T>(string key, IEnumerable<T> values);
    Task<T> GetAsync<T>(string key);
    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire);
    IEnumerable<string> GetKeys(string? pattern);
    Task DeleteAsync(string key);
    void DeleteAll();
    void DeleteAllWithPrefix(string prefix);
    Task<bool> IsExistsAsync(string key);
}