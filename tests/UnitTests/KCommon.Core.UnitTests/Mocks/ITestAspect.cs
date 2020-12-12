namespace KCommon.Core.UnitTests.Mocks
{
    public interface ITestAspect
    {
        [MethodInterceptor]
        bool T();
    }
}