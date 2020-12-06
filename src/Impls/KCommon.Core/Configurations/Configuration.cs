using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using KCommon.Core.Utilities;

namespace KCommon.Core.Configurations
{
    public class Configuration
    {
        private static Configuration Instance { get; set; }

        private Configuration()
        {
        }

        public static Configuration Create()
        {
            Instance = new Configuration();
            return Instance;
        }

        public Configuration SetDefault<TService, TImplementer>(string serviceName = null,
            LifeStyle life = LifeStyle.Singleton)
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

        public Configuration RegisterUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var loggerFactory = ObjectContainer.Resolve<ILoggerFactory>();

                if (loggerFactory != null)
                {
                    var logger = loggerFactory.Create(GetType().FullName);
                    if (logger != null)
                    {
                        logger.ErrorFormat("Unhandled exception: {0}", e.ExceptionObject);
                    }
                }
            };

            return this;
        }

        public Configuration RegisterCommonComponents()
        {
            SetDefault<ILoggerFactory, EmptyLoggerFactory>();
            SetDefault<IBinarySerializer, EmptyBinarySerializer>();
            SetDefault<IJsonSerializer, EmptyJsonSerializer>();
            SetDefault<ICache, EmptyCache>();
            SetDefault<IHttpService, EmptyHttpService>();

            return this;
        }

        public Configuration RegisterScannedComponents(params Assembly[] assemblies)
        {
            var registeredTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(TypeUtils.IsComponent))
                {
                    if (!registeredTypes.Contains(type))
                    {
                        RegisterComponentType(type);
                    }
                }
            }

            return this;
        }

        public Configuration BuildContainer()
        {
            ObjectContainer.Build();
            return this;
        }

        private void RegisterComponentType(Type type)
        {
            var life = ParseComponentLifeStyle(type);
            ObjectContainer.RegisterType(type, null, life);

            foreach (var interfaceType in type.GetInterfaces())
            {
                ObjectContainer.RegisterType(interfaceType, type, null, life);
            }
        }

        private LifeStyle ParseComponentLifeStyle(Type type)
        {
            var attributes = type.GetCustomAttributes<ComponentAttribute>(false);
            if (attributes != null && attributes.Any())
            {
                return attributes.First().LifeStyle;
            }

            return LifeStyle.Singleton;
        }
    }
}