using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Polly;
using Polly.Fallback;
using Polly.Wrap;

namespace KCommon.Core.AOP
{
    public class HystrixCommandAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 最多重试几次，如果为0则不重试
        /// </summary>
        public int MaxRetryTimes { get; set; } = 0;

        /// <summary>
        /// 重试间隔的毫秒数
        /// </summary>
        public int RetryIntervalMilliseconds { get; set; } = 100;

        /// <summary>
        /// 是否启用熔断
        /// </summary>
        public bool EnableCircuitBreaker { get; set; } = false;

        /// <summary>
        /// 熔断前出现允许错误几次
        /// </summary>
        public int ExceptionsAllowedBeforeBreaking { get; set; } = 3;

        /// <summary>
        /// 熔断多长时间（毫秒）
        /// </summary>
        public int MillisecondsOfBreak { get; set; } = 1000;

        /// <summary>
        /// 执行超过多少毫秒则认为超时（0表示不检测超时）
        /// </summary>
        public int TimeOutMilliseconds { get; set; } = 0;

        private static ConcurrentDictionary<MethodInfo, IAsyncPolicy> policies = new ConcurrentDictionary<MethodInfo, IAsyncPolicy>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fallBackMethod">降级的方法名</param>
        public HystrixCommandAttribute(string fallBackMethod)
        {
            FallBackMethod = fallBackMethod;
        }

        public string FallBackMethod { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            //一个HystrixCommand中保持一个policy对象即可
            //其实主要是CircuitBreaker要求对于同一段代码要共享一个policy对象
            //根据反射原理，同一个方法的MethodInfo是同一个对象，但是对象上取出来的HystrixCommandAttribute
            //每次获取的都是不同的对象，因此以MethodInfo为Key保存到policies中，确保一个方法对应一个policy实例
            policies.TryGetValue(context.ServiceMethod, out IAsyncPolicy policy);
            lock (policies)//因为Invoke可能是并发调用，因此要确保policies赋值的线程安全
            {
                if (policy == null)
                {
                    policy = Policy.NoOpAsync();

                    if (EnableCircuitBreaker)
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, TimeSpan.FromMilliseconds(MillisecondsOfBreak)));
                    }
                    if (TimeOutMilliseconds > 0)
                    {
                        policy = policy.WrapAsync(Policy.TimeoutAsync(() => TimeSpan.FromMilliseconds(TimeOutMilliseconds), Polly.Timeout.TimeoutStrategy.Pessimistic));
                    }
                    if (MaxRetryTimes > 0)
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().WaitAndRetryAsync(MaxRetryTimes, i => TimeSpan.FromMilliseconds(RetryIntervalMilliseconds)));
                    }

                    AsyncFallbackPolicy policyFallBack = Policy
                        .Handle<Exception>()
                        .FallbackAsync(async (ctx, t) =>
                        {
                            AspectContext aspectContext = (AspectContext)ctx["aspectContext"];
                            //var fallBackMethod = context.ServiceMethod.DeclaringType.GetMethod(this.FallBackMethod);
                            //merge this issue: https://github.com/yangzhongke/RuPeng.HystrixCore/issues/2
                            var fallBackMethod = context.ImplementationMethod.DeclaringType.GetMethod(this.FallBackMethod);
                            Object fallBackResult = fallBackMethod.Invoke(context.Implementation, context.Parameters);
                            //不能如下这样，因为这是闭包相关，如果这样写第二次调用Invoke的时候context指向的
                            //还是第一次的对象，所以要通过Polly的上下文来传递AspectContext
                            //context.ReturnValue = fallBackResult;
                            aspectContext.ReturnValue = fallBackResult;
                        }, async (ex, t) => { });

                    policy = policyFallBack.WrapAsync(policy);
                    //放入
                    policies.TryAdd(context.ServiceMethod, policy);
                }
            }

            //把本地调用的AspectContext传递给Polly，主要给FallbackAsync中使用，避免闭包的坑
            Context pollyCtx = new Context();
            pollyCtx["aspectContext"] = context;
            
            await policy.ExecuteAsync(ctx => next(context), pollyCtx);
        }
    }
}