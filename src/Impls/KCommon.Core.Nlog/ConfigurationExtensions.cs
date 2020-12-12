using KCommon.Core.Abstract.Logging;
using KCommon.Core.Configurations;

namespace KCommon.Core.Nlog
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use NLog as the logger.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseNlog(this Configuration configuration)
        {
            configuration.SetDefault<ILoggerFactory, NlogLoggerFactory>(new NlogLoggerFactory());
            return configuration;
        }
    }
}