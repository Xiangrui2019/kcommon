using KCommon.Core.JsonNet;
using KCommon.Core.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Serializing
{
    [TestClass]
    public class JsonSerializerTest
    {
        [TestMethod]
        public void TestSerialize()
        {
            var jsonSerializer = new TextJsonSerializer();

            var r = jsonSerializer.Serialize(new ValueObject
            {
                A = 10,
                B = 20,
            });
            
            Assert.AreNotEqual(r, null);
        }
        
        [TestMethod]
        public void TestSerializeJsonNet()
        {
            var jsonSerializer = new NewtonsoftJsonSerializer();

            var r = jsonSerializer.Serialize(new ValueObject
            {
                A = 10,
                B = 20,
            });
            
            Assert.AreNotEqual(r, null);
        }
        
        [TestMethod]
        public void TestDeSerialize()
        {
            var jsonSerializer = new TextJsonSerializer();

            var r = jsonSerializer.Serialize(new ValueObject
            {
                A = 10,
                B = 20,
            });
            
            Assert.AreNotEqual(r, null);

            var s = jsonSerializer.Deserialize<ValueObject>(r);
            
            Assert.AreEqual(s.A, 10);
        }
        
        [TestMethod]
        public void TestDeSerializeJsonNet()
        {
            var jsonSerializer = new NewtonsoftJsonSerializer();

            var r = jsonSerializer.Serialize(new ValueObject
            {
                A = 10,
                B = 20,
            });
            
            Assert.AreNotEqual(r, null);
            
            var s = jsonSerializer.Deserialize<ValueObject>(r);
            Assert.AreEqual(s.A, 10);
        }
    }

    public class ValueObject
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}