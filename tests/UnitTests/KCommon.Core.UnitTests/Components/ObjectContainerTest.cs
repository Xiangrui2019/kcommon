using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Components
{
    [TestClass]
    public class ObjectContainerTest
    {
        [TestMethod]
        public void TestBasic()
        {
            ObjectContainer.SetContainer(new MockObjectContainer());
            ObjectContainer.Build();
        }

        [TestMethod]
        public void TestAutofac()
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer());
            ObjectContainer.Register<MockTestService, MockTestService>();
            ObjectContainer.Build();

            var service = ObjectContainer.Resolve<MockTestService>();
            Assert.AreNotEqual(service, null);
        }
    }
}