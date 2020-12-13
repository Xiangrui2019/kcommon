using System;
using KCommon.Core.Abstract.Serializing;

namespace KCommon.Core.Serializing
{
    public class NotImplementedMessagePackSerializer : IMessagePackSerializer
    {
        public byte[] Serialize(object obj)
        {
            throw new NotSupportedException($"{GetType().FullName} does not support serializing object.");
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            throw new NotSupportedException($"{GetType().FullName} does not support deserializing object.");
        }
    }
}