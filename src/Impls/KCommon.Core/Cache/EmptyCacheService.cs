using KCommon.Core.Abstract.Cache;
using System;
using System.Threading.Tasks;

namespace KCommon.Core.Cache
{
    public class EmptyCacheService : ICacheService
    {
        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public void Clear(string key)
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
