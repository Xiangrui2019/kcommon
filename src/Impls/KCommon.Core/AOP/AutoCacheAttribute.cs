using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace KCommon.Core.AOP
{
    public class AutoCacheAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}