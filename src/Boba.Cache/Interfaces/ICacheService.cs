namespace Boba.Cache;

/// <summary>
/// Defines methods for managing cache operations.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Adds an item to the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to add.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The item to add to the cache.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync<T>(string key, T value);

    /// <summary>
    /// Adds a range of items to the cache.
    /// </summary>
    /// <typeparam name="T">The type of the items to add.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="values">The items to add to the cache.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddRangeAsync<T>(string key, IEnumerable<T> values);

    /// <summary>
    /// Gets an item from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to get.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <returns>A task representing the asynchronous operation, with the item from the cache as the result.</returns>
    Task<T> GetAsync<T>(string key);

    /// <summary>
    /// Gets an item from the cache or creates it if it doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the item to get or create.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="acquire">The function to create the item if it doesn't exist in the cache.</param>
    /// <returns>A task representing the asynchronous operation, with the item from the cache as the result.</returns>
    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire);

    /// <summary>
    /// Gets all cache keys that match the specified pattern.
    /// </summary>
    /// <param name="pattern">The pattern to match against cache keys.</param>
    /// <returns>An enumerable collection of matching cache keys.</returns>
    IEnumerable<string> GetKeys(string? pattern);

    /// <summary>
    /// Deletes an item from the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(string key);

    /// <summary>
    /// Deletes all items from the cache.
    /// </summary>
    void DeleteAll();

    /// <summary>
    /// Deletes all items from the cache that have the specified prefix.
    /// </summary>
    /// <param name="prefix">The prefix to match against cache keys.</param>
    void DeleteAllWithPrefix(string prefix);

    /// <summary>
    /// Determines whether an item exists in the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating whether the item exists.</returns>
    Task<bool> IsExistsAsync(string key);
}