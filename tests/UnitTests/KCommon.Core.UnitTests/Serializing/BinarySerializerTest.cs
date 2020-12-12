using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Serializing
{
    [TestClass]
    public class BinarySerializerTest
    {
        [TestInitialize]
        public void InitFramework()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .BuildContainer();
        }

        [TestMethod]
        public void TestSerialize()
        {
            var binarySerializer = ObjectContainer.Resolve<IBinarySerializer>();

            var b = binarySerializer.Serialize(new TestModel
            {
                A = 10,
                B = 20
            });
            
            Assert.IsNotNull(b);
        }
        
        [TestMethod]
        public void TestDeSerialize()
        {
            var binarySerializer = ObjectContainer.Resolve<IBinarySerializer>();

            var b = binarySerializer.Serialize(new TestModel
            {
                A = 10,
                B = 20
            });

            var r = binarySerializer.Deserialize<TestModel>(b);
            
            Assert.AreEqual(r.A, 10);
            Assert.AreEqual(r.B, 20);
        }
    }
}