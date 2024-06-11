using Boba.Cache;
using Boba.Cache.Memory;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for setting up Boba memory cache services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionService
{
    /// <summary>
    /// Adds Boba memory cache services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    /// <remarks>
    /// This method registers a custom implementation of <see cref="ICacheService"/> that uses in-memory caching.
    /// </remarks>
    public static IServiceCollection UseBobaMemoryCacheServices(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}