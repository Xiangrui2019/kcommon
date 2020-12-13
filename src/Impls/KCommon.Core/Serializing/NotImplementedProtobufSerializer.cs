using KCommon.Core.Abstract.Serializing;

namespace KCommon.Core.Serializing
{
    public class NotImplementedProtobufSerializer : IProtobufSerializer
    {
        public byte[] Serialize(object obj)
        {
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}