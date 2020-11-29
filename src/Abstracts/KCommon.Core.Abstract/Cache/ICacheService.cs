using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.Abstract.Cache
{
    public interface ICacheService
    {
        T GetAndCache<T>(string cacheKey, Func<T> backup, int cachedMinutes = 20);
        Task<T> GetAndCacheAsync<T>(string cacheKey, Func<Task<T>> backup, int cachedMinutes = 20);
        void Clear(string key);
    }
}
