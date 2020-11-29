using System.Net.Http;
using KCommon.Core.Abstract.Cache;
using KCommon.Core.Abstract.Components;
using KCommon.Core.Abstract.Http;
using KCommon.Core.Abstract.Logging;
using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Cache;
using KCommon.Core.Components;
using KCommon.Core.Http;
using KCommon.Core.Logging;
using KCommon.Core.Serializing;

namespace KCommon.Core.Configurations
{
    public class Configuration
    {
        private static Configuration Instance { get; set; }

        private Configuration() { }

        public static Configuration Create()
        {
            Instance = new Configuration();
            return Instance;
        }
        
        public Configuration SetDefault<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(serviceName, life);
            return this;
        }
        public Configuration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        }

        public Configuration AddCommonComponents()
        {
            SetDefault<ILoggerFactory, EmptyLoggerFactory>();
            SetDefault<IBinarySerializer, EmptyBinarySerializer>();
            SetDefault<IJsonSerializer, EmptyJsonSerializer>();
            SetDefault<ICacheService, EmptyCacheService>();
            SetDefault<HttpClient, HttpClient>();
            SetDefault<IHttpService, HttpService>();
            return this;
        }
        
        public Configuration BuildContainer()
        {
            ObjectContainer.Build();
            return this;
        }
    }
}