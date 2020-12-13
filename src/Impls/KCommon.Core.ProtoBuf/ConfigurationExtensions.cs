using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Configurations;

namespace KCommon.Core.ProtoBuf
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use ProtobufNET as the protobuf serializer.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseTextJson(this Configuration configuration)
        {
            configuration.SetDefault<IProtobufSerializer, ProtoBufSerializer>(new ProtoBufSerializer());
            return configuration;
        }
    }
}