using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KCommon.Core.Abstract.Caching;
using KCommon.Core.Abstract.Components;
using KCommon.Core.Abstract.Http;
using KCommon.Core.Abstract.Logging;
using KCommon.Core.Abstract.Serializing;
using KCommon.Core.Caching;
using KCommon.Core.Components;
using KCommon.Core.Http;
using KCommon.Core.Logging;
using KCommon.Core.Serializing;
using KCommon.Core.Utilities;

namespace KCommon.Core.Configurations
{
    public class Configuration
    {
        /// <summary>Provides the singleton access instance.
        /// </summary>
        public static Configuration Instance { get; private set; }

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

        public Configuration RegisterCommonComponents()
        {
            SetDefault<ILoggerFactory, EmptyLoggerFactory>();
            SetDefault<ILogger, EmptyLogger>();
            SetDefault<IJsonSerializer, NotImplementedJsonSerializer>();
            SetDefault<IBinarySerializer, DefaultBinarySerializer>();
            SetDefault<IMessagePackSerializer, NotImplementedMessagePackSerializer>();
            SetDefault<IProtobufSerializer, NotImplementedProtobufSerializer>();
            SetDefault<IHttpService, EmptyHttpService>();
            SetDefault<ICache, EmptyCache>();
            SetDefault<Cannon, Cannon>();
            
            return this;
        }
        
        public Configuration RegisterUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var loggerFactory = ObjectContainer.Resolve<ILoggerFactory>();

                if (loggerFactory == null) return;
                var logger = loggerFactory.Create(GetType().FullName);
                logger?.ErrorFormat("Unhandled exception: {0}", e.ExceptionObject);
            };

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