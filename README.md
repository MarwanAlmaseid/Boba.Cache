# Boba.Cache

Boba.Cache is an ASP.NET Core library that simplifies cache management. It offers interfaces to handle all cache operations (get, set, remove,...), generate keys, and manage cache entries.

## Getting Started

### Installation

Boba.Cache is conveniently available on NuGet. Simply install the provider package corresponding to your database.

Choose your data store option and follow its documentation to install the right package.
- Memory Cache: [Boba.Cache.Microsoft.DependencyInjection](https://github.com/MarwanAlmaseid/Boba.Cache/tree/master/src/Boba.Cache.Memory.Microsoft.DependencyInjection)
- Redis Cache: [Boba.Cache.Redis.Microsoft.DependencyInjection](https://github.com/MarwanAlmaseid/Boba.Cache/tree/master/src/Boba.Cache.Redis.Microsoft.DependencyInjection)


Utilize the `--version` option to specify a preview version for installation if needed.

### Basic Usage
Harnessing the power of Boba.Cache is straightforward. Follow these steps to get started:

#### Registration: 
Begin by registering Boba.Cache in your `program.cs`. Presently, we support In Memory Cache, Redis Cache.

In Memory Cache
```c#
builder.Services.AddBobaCacheServices().UseBobaMemoryCacheServices();

```

Redis Cahce

```c#
builder.Services.AddBobaCacheServices().UseBobaRedisCacheServices("YOUR_REDIS_CONNECTION_STRING");

```

#### Management:

We have 3 main Interfaces

1- ICacheKeyService
 Provides a way to construct a unique key for values caching, There is unlimited params to construct a unique key
```c#
private readonly ICacheKeyService _cacheKeyService;
	
public YourConstructor(ICacheKeyService cacheKeyService)
{
    _cacheKeyService = cacheKeyService;
}

//Use this code inside your method
var key = _cacheKeyService.PrepareKey("YOUR_PREFIX" , "X" , "Y" , "Z");
```

2- ICacheManager
 Provides hassle-free Get/Store values to cash, by using a function getting the value from the cash in case exists, and executing the query then store in the cash in case not exists. 
```c#
private readonly ICacheKeyService _cacheKeyService;
private readonly ICacheManager _cacheManager;
private readonly IApplicationDbContext _context;

public YourConstructor(ICacheKeyService cacheKeyService, ICacheManager cacheManager, IApplicationDbContext _context)
{
    _cacheKeyService = cacheKeyService;
    _cacheManager = cacheManager;
    IApplicationDbContext context,

}


//Use this code inside your method

var key = _cacheKeyService.PrepareKey("YOUR_PREFIX" , "X" , "Y" , "Z");

var query = _context.Users
            .Where(c => c.Active)
            .Include(c => c.Roles)
            .OrderBy(c => c.Id)
            .ThenByDescending(c => c.Created)
            .AsNoTracking();

var result = await _cacheManager.GetAsync(key, async () =>
            {
                var users = await query.ToListAsync(cancellationToken: cancellationToken);

                return users;
            });

```

3- ICacheService
 Provides some API to deal with the cached values and apply crud operations on it, this gives more control on caching comparing to ICacheManager but require extra work and manual handle of adding and checking in case the value exists etc.
```c#
private readonly ICacheService _cacheService;
	
public YourConstructor( ICacheService cacheService)
{
    _cacheService = _cacheService;
}

// Add 

var key = "YOUR_KEY"

var isExists = _cacheService.IsExistsAsync(key)

if(isExists) {
    var value = _cacheService.GetAsync<string>(key)
}else {
    var value = //get from your data store
    await _cacheService.AddAsync<string>(key,value)
}

// Delete
_cacheService.DeleteAllWithPrefix("YOUR_PREFIX");
```

### Available Injection Approaches

- Microsoft Dependency Injection

### Contributing

Contributions are always welcome!

See `contributing.md` for ways to get started.

Please adhere to this project's `code of conduct`.


### Upcoming Features
- Support more injection approaches.
- Use mixed caching approaches depending on the availability.

### Versions
The main branch is now on .NET 8.0. Previous versions are not available at this time.

### License
This project is licensed under the [MIT](https://choosealicense.com/licenses/mit/) license.

### Support
If you encounter any issues or have questions, please feel free to [raise a new issue](https://github.com/MarwanAlmaseid/Boba.Cache/issues).

### Technologies

 - [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)
