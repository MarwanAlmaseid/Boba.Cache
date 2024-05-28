namespace Boba.Cache;

public interface ICacheService
{
    string Add<T>(string key, T value);
    string AddRange<T>(string key, IEnumerable<T> values);
    T Get<T>(string key);
    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire);
    IEnumerable<string> GetKeys(string? pattern);
    void Delete(string key);
    void DeleteAll();
    void DeleteAllWithPrefix(string prefix);
    bool IsExists(string key);
}