using Boba.Cache;
using Boba.Cache.Redis;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for setting up Boba Redis cache services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionService
{
    /// <summary>
    /// Adds StackExchange.Redis and a custom cache service to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="setupAction">An optional action to configure the Redis cache options.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    /// <remarks>
    /// This method adds a Redis cache using StackExchange.Redis and registers a custom implementation of <see cref="ICacheService"/>.
    /// </remarks>
    public static IServiceCollection UseBobaRedisCacheServices(this IServiceCollection services, string redisConnectionString)
    {
        services.AddSingleton<ICacheService>(provider => new CacheService(redisConnectionString));

        return services;
    }
}