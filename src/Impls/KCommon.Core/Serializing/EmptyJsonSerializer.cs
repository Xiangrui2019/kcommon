using System;
using KCommon.Core.Abstract.Serializing;

namespace KCommon.Core.Serializing
{
    public class EmptyJsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            throw new NotSupportedException($"{GetType().FullName} does not support serializing object.");
        }
        public object Deserialize(string value, Type type)
        {
            throw new NotSupportedException($"{GetType().FullName} does not support deserializing object.");
        }
        public T Deserialize<T>(string value) where T : class
        {
            throw new NotSupportedException($"{GetType().FullName} does not support deserializing object.");
        }
    }
}