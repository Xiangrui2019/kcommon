using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Caching;
using Newtonsoft.Json;

namespace KCommon.Core.RedisCache
{
    public class InRedisCache : ICache
    {
        private readonly IDatabase _cache;

        public InRedisCache(ConnectionMultiplexer factory)
        {
            _cache = factory.GetDatabase();
        }

        public void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            var data = JsonConvert.SerializeObject(cacheValue);
            
            if (cachedMinutes != 0)
                _cache.StringSet(cacheKey, data, TimeSpan.FromMinutes(cachedMinutes));
            else
                _cache.StringSet(cacheKey, data);
        }

        public T Get<T>(string cacheKey)
        {
            var data = _cache.StringGet(cacheKey);

            return data == "" ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

        public async Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            var data = JsonConvert.SerializeObject(cacheValue);
            
            if (cachedMinutes != 0)
                await _cache.StringSetAsync(cacheKey, data, TimeSpan.FromMinutes(cachedMinutes));
            else
                await _cache.StringSetAsync(cacheKey, data);
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var data = await _cache.StringGetAsync(cacheKey);
            return data == "" ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            var (resultValue, status) = await TryGetAsync<T>(cacheKey);
            if (status == false || resultValue == null)
            {
                resultValue = await backup();
                
                await SetAsync(cacheKey, resultValue, cachedMinutes);
            }

            return resultValue;
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            var (resultValue, status) = TryGet<T>(cacheKey);
            if (status == false || resultValue == null)
            {
                resultValue = backup();

                Set<T>(cacheKey, resultValue, cachedMinutes);
            }

            return resultValue;
        }

        public (T, bool) TryGet<T>(string cacheKey)
        {
            var result = _cache.StringGet(cacheKey);

            if (result == "")
            {
                return (default(T), false);
            }

            return (JsonConvert.DeserializeObject<T>(result), true);
        }

        public async Task<(T, bool)> TryGetAsync<T>(string cacheKey)
        {
            var result = await _cache.StringGetAsync(cacheKey);

            if (result == "")
            {
                return (default(T), false);
            }

            return (JsonConvert.DeserializeObject<T>(result), true);
        }

        public void Clear(string cacheKey)
        {
            _cache.StringSet(cacheKey, "");
        }
    }
}