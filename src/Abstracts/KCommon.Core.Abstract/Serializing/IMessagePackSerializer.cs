using System;

namespace KCommon.Core.Abstract.Serializing
{
    public interface IMessagePackSerializer
    {
        byte[] Serialize(object obj);
        object Deserialize(byte[] data, Type type);
        T Deserialize<T>(byte[] data) where T : class;
    }
}