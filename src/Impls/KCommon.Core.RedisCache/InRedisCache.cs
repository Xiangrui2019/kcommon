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