using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using KCommon.Core.Abstract.Caching;
using KCommon.Core.Components;

namespace KCommon.Core.AOP
{
    public class AutoCacheAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 缓存时间, 0为不缓存
        /// </summary>
        public int CachedMinutes { get; set; } = 0;
        
        private static ICache _cache = ObjectContainer.Resolve<ICache>();
        
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey = GetCacheKey(context.ServiceMethod, context.Parameters);
            var (result, status) = await _cache.TryGetAsync<object>(cacheKey);

            if (status == true)
            {
                context.ReturnValue = result;
                return;
            }
            
            await next(context);
            await _cache.SetAsync(cacheKey, context.ReturnValue, CachedMinutes);
        }

        private string GetCacheKey(MethodInfo methodInfo, object[] param)
            => $"AOP_AutoCache_{methodInfo.Name}_{methodInfo.DeclaringType}_{methodInfo}_{string.Join("_", param)}";
    }
}