using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Boba.Cache.Redis
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cacheDb;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private const string  RedisConncetionStringKey = "Redis";
       
        public CacheService(IConfiguration configuration)
        {
            var redisConnection = configuration.GetConnectionString(RedisConncetionStringKey) ??
                throw new Exception("Redis connection string not found on the appsetting ConncetionString section, Kindly add your Redis connection string");
            
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnection);
            _cacheDb = _connectionMultiplexer.GetDatabase();
        }

        public async Task AddAsync<T>(string key, T value)
        {
            string jsonValue = JsonConvert.SerializeObject(value);

            await _cacheDb.StringSetAsync(key, jsonValue);
        }

        public async Task AddRangeAsync<T>(string key, IEnumerable<T> values)
        {
            var jsonValues = JsonConvert.SerializeObject(values);

            await _cacheDb.StringSetAsync(key, jsonValues);
        }

        public async Task DeleteAsync(string key)
        {
            await _cacheDb.KeyDeleteAsync(key);
        }

        public async void DeleteAll()
        {
            var server = GetServer();

            await server.FlushAllDatabasesAsync();
        }

        public async void DeleteAllWithPrefix(string prefix)
        {
            var keysWithPrefix = GetKeys(null).Where(k => k.StartsWith(prefix));

            foreach (var key in keysWithPrefix)
            {
                await DeleteAsync(key);
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cacheDb.StringGetAsync(key);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public IEnumerable<string> GetKeys(string? pattern)
        {
            var server = GetServer();
            var keys = server.Keys(pattern: pattern ?? "*").Select(k => k.ToString());

            return keys;
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire)
        {
            if (await IsExistsAsync(key))
            {
                return await GetAsync<T>(key);
            }

            var result = await acquire();
            await AddAsync(key, result);

            return result;
        }

        public async Task<bool> IsExistsAsync(string key)
        {
            return await _cacheDb.KeyExistsAsync(new RedisKey(key));
        }

        private IServer GetServer()
        {
            var endPoint = _connectionMultiplexer.GetEndPoints().FirstOrDefault();
            var server = _connectionMultiplexer.GetServer(endPoint);

            return server;
        }
    }
}
