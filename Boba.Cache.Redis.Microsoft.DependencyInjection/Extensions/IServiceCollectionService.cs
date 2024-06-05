using Boba.Cache;
using Boba.Cache.Redis;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionService
{
    public static IServiceCollection UseBobaRedisCacheServices(this IServiceCollection services, Action<RedisCacheOptions> setupAction = null)
    {
        setupAction ??= opt => { };

        services.AddStackExchangeRedisCache(setupAction);
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}