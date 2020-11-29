using KCommon.Core.Abstract.Cache;
using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Utilities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace KCommon.Core.MemoryCache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IJsonSerializer _jsonSerializer;

        public RedisCacheService(
            IDistributedCache cache,
            IJsonSerializer jsonSerializer)
        {
            _cache = cache;
            _jsonSerializer = jsonSerializer;
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            var result = _cache.GetString(cacheKey);
            var resultValue = ExceptionHelper.EatException(
                () => (T)_jsonSerializer.Deserialize(result, typeof(T)));

            if (result == null || result == "")
            {
                resultValue = backup();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cachedMinutes));

                result = _jsonSerializer.Serialize(resultValue);

                _cache.SetString(cacheKey, result, cacheEntryOptions);
            }

            return resultValue;
        }

        public async Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            var result = await _cache.GetStringAsync(cacheKey);
            var resultValue = ExceptionHelper.EatException(
                () => (T)_jsonSerializer.Deserialize(result, typeof(T)));

            if (result == null || result == "")
            {
                resultValue = await backup();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cachedMinutes));

                result = _jsonSerializer.Serialize(resultValue);

                await _cache.SetStringAsync(cacheKey, result, cacheEntryOptions);
            }

            return resultValue;
        }

        public void Clear(string key)
        {
            _cache.Remove(key);
        }
    }
}