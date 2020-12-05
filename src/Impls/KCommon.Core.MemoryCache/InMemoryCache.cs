using KCommon.Core.Abstract.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.MemoryCache
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Clear(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public T Get<T>(string cacheKey)
            => _cache.Get<T>(cacheKey);

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            if (!_cache.TryGetValue(cacheKey, out T resultValue) || resultValue == null)
            {
                resultValue = backup();

                var cacheEntryOptions = new MemoryCacheEntryOptions();
                if (cachedMinutes != 0)
                {
                    cacheEntryOptions.SetSlidingExpiration(
                        TimeSpan.FromMinutes(cachedMinutes));
                }

                _cache.Set(cacheKey, resultValue, cacheEntryOptions);
            }

            return resultValue;
        }

        public async Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            if (!_cache.TryGetValue(cacheKey, out T resultValue) || resultValue == null)
            {
                resultValue = await backup();

                var cacheEntryOptions = new MemoryCacheEntryOptions();
                if (cachedMinutes != 0)
                {
                    cacheEntryOptions.SetSlidingExpiration(
                        TimeSpan.FromMinutes(cachedMinutes));
                }

                _cache.Set(cacheKey, resultValue, cacheEntryOptions);
            }

            return resultValue;
        }

        public Task<T> GetAsync<T>(string cacheKey)
            => Task.FromResult(Get<T>(cacheKey));

        public void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();

            if (cachedMinutes != 0)
            {
                cacheEntryOptions.SetSlidingExpiration(
                    TimeSpan.FromMinutes(cachedMinutes));
            }

            _cache.Set<T>(cacheKey, cacheValue, cacheEntryOptions);
        }

        public Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            Set<T>(cacheKey, cacheValue, cachedMinutes);

            return Task.CompletedTask;
        }
    }
}
