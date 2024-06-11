using Boba.Cache;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for setting up Boba cache services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionService
{
    /// <summary>
    /// Adds Boba cache services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddBobaCacheServices(this IServiceCollection services)
    {
        services.AddTransient<ICacheKeyService, CacheKeyService>();
        services.AddTransient<ICacheManager, CacheManager>();

        return services;
    }
}