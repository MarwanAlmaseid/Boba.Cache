using Boba.Cache;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionService
{
    public static IServiceCollection AddBobaCacheServices(this IServiceCollection services)
    {
        services.AddTransient<ICacheKeyService, CacheKeyService>();
        services.AddTransient<ICacheManager, CacheManager>();

        return services;
    }
}