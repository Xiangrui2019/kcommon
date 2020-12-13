using System;
using System.IO;
using KCommon.Core.Abstract.Serializing;
using ProtoBuf;

namespace KCommon.Core.ProtoBuf
{
    /// <summary>ProtoBuf-NET implementationof IProtobufSerializer.
    /// </summary>
    public class ProtoBufSerializer : IProtobufSerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            
            using var memoryStream = new MemoryStream();
            Serializer.Serialize(memoryStream, obj);

            return memoryStream.ToArray();
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            using var memoryStream = new MemoryStream(data);
            
            return Serializer.Deserialize<T>(memoryStream);
        }
    }
}