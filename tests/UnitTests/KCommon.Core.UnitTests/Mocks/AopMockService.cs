using System;
using System.Threading.Tasks;
using KCommon.Core.AOP;

namespace KCommon.Core.UnitTests.Mocks
{
    public class AopMockService
    {
        [HystrixCommand(EnableFallBack = true, FallBackMethod = nameof(Hello1FallBackAsync), MaxRetryTimes = 3, EnableCircuitBreaker = true)]
        public virtual async Task<string> HelloAsync(string name)//需要是虚方法
        {
            Console.WriteLine("尝试执行HelloAsync" + name);
            String s = null;
            s.ToString();
            return "ok" + name;
        }

        [HystrixCommand(EnableFallBack = true, FallBackMethod = nameof(Hello2FallBackAsync))]
        public virtual async Task<string> Hello1FallBackAsync(string name)
        {
            Console.WriteLine("Hello降级1" + name);
            String s = null;
            s.ToString();
            return "fail_1";
        }

        public virtual async Task<string> Hello2FallBackAsync(string name)
        {
            Console.WriteLine("Hello降级2" + name);

            return "fail_2";
        }

        [HystrixCommand(EnableFallBack = true, FallBackMethod = nameof(AddFall))]
        public virtual int Add(int i, int j)
        {
            String s = null;
            //s.ToString();
            return i + j;
        }
        public int AddFall(int i, int j)
        {
            return 0;
        }
        
        [HystrixCommand(EnableFallBack = true, FallBackMethod = nameof(AddTestFall))]
        public virtual int AddTest(int i, int j)
        {
            String s = null;
            s.ToString();
            return i + j;
        }
        public int AddTestFall(int i, int j)
        {
            return 0;
        }

        [HystrixCommand(EnableFallBack = true, FallBackMethod = nameof(TestFallBack))]
        public virtual int Test(int i)
        {
            var a = 0;
            var s = 1 / a;
            Console.WriteLine("Test" + i);

            return i;
        }

        public virtual int TestFallBack(int i)
        {
            return 0;
        }
        
        [HystrixCommand(MaxRetryTimes = 8)]
        public virtual int Test2(int i)
        {
            Console.WriteLine("Test" + i);
            var a = 0;
            var s = 1 / a;
            
            return i;
        }
    }
}