using System;
using System.Threading.Tasks;

namespace KCommon.Core.Abstract.Caching
{
    public interface ICache
    {
        void Set<T>(string cacheKey, T cacheValue, int cachedMinutes = 20);
        T Get<T>(string cacheKey);
        Task SetAsync<T>(string cacheKey, T cacheValue, int cachedMinutes = 20);
        Task<T> GetAsync<T>(string cacheKey);

        Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20);
        T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20);

        (T, bool) TryGet<T>(string cacheKey);
        Task<(T, bool)> TryGetAsync<T>(string cacheKey);

        void Clear(string cacheKey);
    }
}