# Boba.Cache.Redis.Microsoft.DependencyInjection

`Boba.Cache.Redis.Microsoft.DependencyInjection` is a library that provides extension methods for setting up Boba Redis cache services in an ASP.NET Core application using `IServiceCollection`.

## Installation

To install the library, use the following NuGet package manager command:

```bash
dotnet add package Boba.Cache.Redis.Microsoft.DependencyInjection

Usage
Adding Boba Redis Cache Services
You can add the Boba Redis cache services to your IServiceCollection by using the UseBobaRedisCacheServices extension method. This method configures Redis caching and registers a custom cache service implementation.

Example
Here's an example of how to use the UseBobaRedisCacheServices method in your ASP.NET Core application:

using Boba.Cache.Redis.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.UseBobaRedisCacheServices(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "SampleInstance";
        });

        // Other service configurations
    }

    // Other methods
}


API Reference
IServiceCollectionService
The IServiceCollectionService class provides the following extension method:

UseBobaRedisCacheServices
public static IServiceCollection UseBobaRedisCacheServices(this IServiceCollection services, Action<RedisCacheOptions> setupAction = null)


Parameters
services (IServiceCollection): The collection of service descriptors.
setupAction (Action<RedisCacheOptions>): An optional action to configure the Redis cache options.
Returns
IServiceCollection: The same service collection so that additional calls can be chained.
Remarks
This method adds a Redis cache using StackExchange.Redis and registers a custom implementation of ICacheService.

By following these instructions, you can easily integrate and configure the Boba Redis cache services in your ASP.NET Core application. For more details, refer to the source code and documentation comments within the library.

csharp
