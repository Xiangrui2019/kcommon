using System;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Caching;

namespace KCommon.Core.Caching
{
    public class EmptyCache : ICache
    {
        public void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public (T, bool) TryGet<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public Task<(T, bool)> TryGetAsync<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public void Clear(string cacheKey)
        {
            throw new NotImplementedException();
        }
    }
}