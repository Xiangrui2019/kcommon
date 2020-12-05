using KCommon.Core.Abstract.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.Cache
{
    public class EmptyCache : ICache
    {
        public void Clear(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20)
        {
            throw new NotImplementedException();
        }
    }
}
