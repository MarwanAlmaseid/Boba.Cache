using Boba.Cache;
using Boba.Cache.Memory;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionService
{
    public static IServiceCollection UseBobaMemoryCacheServices(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}