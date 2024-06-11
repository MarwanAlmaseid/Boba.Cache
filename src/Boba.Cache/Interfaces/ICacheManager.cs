namespace Boba.Cache;

/// <summary>
/// Defines methods for managing cache operations with additional management capabilities.
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// Gets an item from the cache, or creates it if it doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the item to get or create.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="acquire">The function to create the item if it doesn't exist in the cache.</param>
    /// <returns>A task representing the asynchronous operation, with the item from the cache as the result.</returns>
    Task<T> GetAsync<T>(string key, Func<Task<T>> acquire);
}