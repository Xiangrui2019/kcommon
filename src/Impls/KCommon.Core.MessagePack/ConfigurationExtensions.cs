using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Configurations;

namespace KCommon.Core.MessagePack
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use MessagePack as the mpkg serializer.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseMessagePack(this Configuration configuration)
        {
            configuration.SetDefault<IMessagePackSerializer, MessagePackSerializer>(new MessagePackSerializer());
            return configuration;
        }
    }
}