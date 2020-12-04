using KCommon.Core.Abstract.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.MemoryCache
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            if (!_cache.TryGetValue(cacheKey, out T resultValue) || resultValue == null)
            {
                resultValue = backup();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cachedMinutes));

                _cache.Set(cacheKey, resultValue, cacheEntryOptions);
            }
            return resultValue;
        }

        public async Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            if (!_cache.TryGetValue(cacheKey, out T resultValue) || resultValue == null)
            {
                resultValue = await backup();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cachedMinutes));

                _cache.Set(cacheKey, resultValue, cacheEntryOptions);
            }

            return resultValue;
        }

        public void Clear(string key)
        {
            _cache.Remove(key);
        }

        public Task SetAsync<T>(string cacheKey, T value, int cachedMinutes)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();

            if (cachedMinutes == default(int) || cachedMinutes == 0)
            {
                cacheEntryOptions
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cachedMinutes));
            }

            _cache.Set<T>(cacheKey, value, cacheEntryOptions);
            return Task.CompletedTask;
        }

        public Task<T> GetAsync<T>(string cacheKey)
            => Task.FromResult(_cache.Get<T>(cacheKey));
    }
}
