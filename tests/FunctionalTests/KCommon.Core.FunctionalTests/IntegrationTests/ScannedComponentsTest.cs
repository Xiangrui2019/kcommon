using KCommon.Core.Abstract.Components;
using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace KCommon.Core.FunctionalTests.IntegrationTests
{
    [TestClass]
    public class ScannedComponentsTest
    {
        [TestInitialize]
        public void InitTest()
        {
            var assemblies = new Assembly[]
            {
                Assembly.Load("KCommon.Core.FunctionalTests")
            };

            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .RegisterScannedComponents(assemblies)
                .BuildContainer();
        }

        [TestMethod]
        public void TestReslove()
        {
            var service = ObjectContainer.Resolve<Test>();

            Assert.AreEqual(service.Tes(), 10);
            Assert.AreEqual(service.Tes2(), 10);
        }

        [TestMethod]
        public void TestInterfaceReslove()
        {
            var service = ObjectContainer.Resolve<ITestInterface>();

            Assert.AreEqual(service.Tes(), 10);
        }
    }

    [Component]
    public class Test
    {
        private readonly ITestInterface _i;

        public Test(ITestInterface i)
        {
            _i = i;
        }

        public int Tes()
        {
            return 10;
        }

        public int Tes2()
        {
            return _i.Tes();
        }
    }

    public interface ITestInterface
    {
        int Tes();
    }

    [Component]
    public class TestInterface : ITestInterface
    {
        public int Tes()
        {
            return 10;
        }
    }
}