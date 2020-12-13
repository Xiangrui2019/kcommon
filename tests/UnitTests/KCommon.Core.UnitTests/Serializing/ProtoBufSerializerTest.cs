using KCommon.Core.ProtoBuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;

namespace KCommon.Core.UnitTests.Serializing
{
    [TestClass]
    public class ProtoBufSerializerTest
    {
        [TestMethod]
        public void TestSerialize()
        {
            var serializer = new ProtoBufSerializer();

            var t = serializer.Serialize(new VPb
            {
                A = 10,
                B = 20,
            });
            
            Assert.AreNotEqual(t, null);
        }
        
        [TestMethod]
        public void TestDeSerialize()
        {
            var serializer = new ProtoBufSerializer();

            var t = serializer.Serialize(new VPb
            {
                A = 10,
                B = 20,
            });

            var r = serializer.Deserialize<VPb>(t);
            
            Assert.AreEqual(r.A, 10);
        }
    }
    
    [ProtoContract]
    public class VPb
    {
        [ProtoMember(1)]
        public int A { get; set; }
        [ProtoMember(2)]
        public int B { get; set; }
    }
}