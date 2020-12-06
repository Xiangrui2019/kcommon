using KCommon.Core.Abstract.Http;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using System.Net.Http;

namespace KCommon.Core.BasicHttp
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use Basic Http as the Http Caller.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseBasicHttp(this Configuration configuration)
        {
            configuration.SetDefault<HttpClient, HttpClient>(new HttpClient());
            configuration.SetDefault<IHttpService, HttpService>();

            return configuration;
        }
    }
}