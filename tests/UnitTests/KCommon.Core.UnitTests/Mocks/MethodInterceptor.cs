using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace KCommon.Core.UnitTests.Mocks
{
    public class MethodInterceptor : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate _)
        {
            context.ReturnValue = true;

            return Task.CompletedTask;
        }
    }
}