using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.JsonNet
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use Json.Net as the json serializer.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseJsonNet(this Configuration configuration)
        {
            configuration.SetDefault<IJsonSerializer, NewtonsoftJsonSerializer>(new NewtonsoftJsonSerializer());
            return configuration;
        }
    }
}
