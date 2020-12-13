using System;
using KCommon.Core.Abstract.Serializing;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace KCommon.Core.Text.Json
{
    /// <summary>Json.Net implementationof IJsonSerializer.
    /// </summary>
    public class TextJsonSerializer : IJsonSerializer
    {
        /// <summary>Serialize an object to json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            return obj == null ? null : JsonSerializer.Serialize(obj);
        }

        /// <summary>Deserialize a json string to an object.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string value, Type type)
        {
            return JsonSerializer.Deserialize(value, type);
        }

        /// <summary>Deserialize a json string to a strong type object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Deserialize<T>(string value) where T : class
        {
            return JsonSerializer.Deserialize<T>(value);
        }
    }
}