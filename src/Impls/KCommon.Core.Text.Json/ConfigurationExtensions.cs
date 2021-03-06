﻿using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Configurations;

namespace KCommon.Core.Text.Json
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use Text Json as the json serializer.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseTextJson(this Configuration configuration)
        {
            configuration.SetDefault<IJsonSerializer, TextJsonSerializer>(new TextJsonSerializer());
            return configuration;
        }
    }
}