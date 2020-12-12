using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Caching;

namespace KCommon.Core.MemoryCache
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<(T, bool)> TryGetAsync<T>(string cacheKey)
            => Task.FromResult(TryGet<T>(cacheKey));

        public void Clear(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.Get<T>(cacheKey);
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            var (resultValue, status) = TryGet<T>(cacheKey);
            if (status == false)
            {
                resultValue = backup();

                Set<T>(cacheKey, resultValue, cachedMinutes);
            }

            return resultValue;
        }

        public (T, bool) TryGet<T>(string cacheKey)
        {
            var status = _cache.TryGetValue(cacheKey, out T resultValue);
            
            return (resultValue, status);
        }

        public async Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            var (resultValue, status) = await TryGetAsync<T>(cacheKey);
            if (status == false)
            {
                resultValue = await backup();
                
                await SetAsync(cacheKey, resultValue, cachedMinutes);
            }

            return resultValue;
        }

        public Task<T> GetAsync<T>(string cacheKey)
        {
            return Task.FromResult(Get<T>(cacheKey));
        }

        public void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();

            if (cachedMinutes != 0)
                cacheEntryOptions.SetSlidingExpiration(
                    TimeSpan.FromMinutes(cachedMinutes));

            _cache.Set<T>(cacheKey, cacheValue, cacheEntryOptions);
        }

        public Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            Set<T>(cacheKey, cacheValue, cachedMinutes);

            return Task.CompletedTask;
        }
    }
}