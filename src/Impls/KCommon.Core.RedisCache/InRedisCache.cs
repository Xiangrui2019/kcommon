using KCommon.Core.Abstract.Cache;
using KCommon.Core.Abstract.Serializing;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.RedisCache
{
    public class InRedisCache : ICache
    {
        private readonly IDatabase _cache;

        public InRedisCache(ConnectionMultiplexer factory)
        {
            _cache = factory.GetDatabase();
        }

        public Task<T> TryGetAsync<T>(string cacheKey)
        {
            var result = _cache.StringGet(cacheKey);
        }

        public void Clear(string cacheKey)
        {
            _cache.StringSet(cacheKey, "");
        }

        public T Get<T>(string cacheKey)
        {
            var result = _cache.StringGet(cacheKey);

            if (result == "") return default(T);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var result = await _cache.StringGetAsync(cacheKey);

            if (result == "") return default(T);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            var result = Get<T>(cacheKey);

            if (result == null)
            {
                result = backup();
                Set<T>(cacheKey, result, cachedMinutes);
            }

            return result;
        }

        public T TryGet<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            var result = await GetAsync<T>(cacheKey);

            if (result == null)
            {
                result = await backup();
                await SetAsync<T>(cacheKey, result, cachedMinutes);
            }

            return result;
        }

        public void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            var result = JsonConvert.SerializeObject(cacheValue);
            _cache.StringSet(cacheKey, result, TimeSpan.FromMinutes(cachedMinutes));
        }

        public async Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            var result = JsonConvert.SerializeObject(cacheValue);
            await _cache.StringSetAsync(cacheKey, result, TimeSpan.FromMinutes(cachedMinutes));
        }
    }
}