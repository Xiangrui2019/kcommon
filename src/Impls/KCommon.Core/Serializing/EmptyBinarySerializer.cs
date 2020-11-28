using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using KCommon.Core.Abstract.Serializing;

namespace KCommon.Core.Serializing
{
    public class EmptyBinarySerializer : IBinarySerializer
    {
        public byte[] Serialize(object obj)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(byte[] data, Type type)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}