using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.JsonNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.FunctionalTests.IntegrationTests
{
    [TestClass]
    public class JsonSerilizingTest
    {
        [TestInitialize]
        public void InitFramework()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseJsonNet()
                .BuildContainer();
        }

        [TestMethod]
        public void TestJsonSerialize()
        {
            var jsonSerliizer = ObjectContainer.Resolve<IJsonSerializer>();

            var result = jsonSerliizer.Serialize(new TestJsonModel {A = 10, B = 20});

            Assert.IsNotNull(result);
            Assert.AreNotEqual(result, "");
        }

        [TestMethod]
        public void TestJsonDeSerialize()
        {
            var jsonSerliizer = ObjectContainer.Resolve<IJsonSerializer>();

            var result = jsonSerliizer.Serialize(new TestJsonModel {A = 10, B = 20});

            Assert.IsNotNull(result);
            Assert.AreNotEqual(result, "");

            var deresult = jsonSerliizer.Deserialize<TestJsonModel>(result);

            Assert.AreEqual(deresult.A, 10);
            Assert.AreEqual(deresult.B, 20);
        }
    }

    internal class TestJsonModel
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}