using MessagePack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessagePackSerializer = KCommon.Core.MessagePack.MessagePackSerializer;

namespace KCommon.Core.UnitTests.Serializing
{
    [TestClass]
    public class MessagePackSerializerTest
    {
        [TestMethod]
        public void TestSerialize()
        {
            var serializer = new MessagePackSerializer();

            var r = serializer.Serialize(new V
            {
                A = 10,
                B = 20,
            });
            
            Assert.AreNotEqual(r, null);
        }
        
        [TestMethod]
        public void TestDeSerialize()
        {
            var serializer = new MessagePackSerializer();

            var t = serializer.Serialize(new V
            {
                A = 10,
                B = 20,
            });
            var r = serializer.Deserialize<V>(t);
            
            Assert.AreEqual(r.A, 10);
        }
    }
    
    [MessagePackObject]
    public class V
    {
        [Key(0)]
        public int A { get; set; }
        [Key(1)]
        public int B { get; set; }
    }
}