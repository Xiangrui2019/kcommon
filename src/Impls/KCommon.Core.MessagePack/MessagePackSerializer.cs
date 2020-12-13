using System;
using KCommon.Core.Abstract.Serializing;


namespace KCommon.Core.MessagePack
{
    /// <summary>MessagePack implementation of IMessagePackSerializer.
    /// </summary>
    public class MessagePackSerializer : IMessagePackSerializer
    {
        public byte[] Serialize(object obj)
        {
            return obj == null ? null : global::MessagePack.MessagePackSerializer.Serialize(obj);
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            return global::MessagePack.MessagePackSerializer.Deserialize<T>(data);
        }
    }
}