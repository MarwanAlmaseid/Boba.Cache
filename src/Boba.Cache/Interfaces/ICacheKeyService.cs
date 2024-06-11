namespace Boba.Cache;

/// <summary>
/// Defines methods for preparing cache keys.
/// </summary>
public interface ICacheKeyService
{
    /// <summary>
    /// Prepares a cache key with the specified base key and parameters.
    /// </summary>
    /// <param name="cacheKey">The base cache key.</param>
    /// <param name="cacheKeyParameters">The parameters to append to the cache key.</param>
    /// <returns>A string representing the prepared cache key.</returns>
    string PrepareKey(string cacheKey, params object[] cacheKeyParameters);
}