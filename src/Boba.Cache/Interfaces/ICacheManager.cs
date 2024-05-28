namespace Boba.Cache;

public interface ICacheManager
{
    Task<T> GetAsync<T>(string key, Func<Task<T>> acquire);
}