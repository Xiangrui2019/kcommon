﻿using Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;

namespace KCommon.Core.Autofac
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use Autofac as the object container.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseAutofac(this Configuration configuration)
        {
            return UseAutofac(configuration, new ContainerBuilder());
        }
        /// <summary>Use Autofac as the object container.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseAutofac(this Configuration configuration, ContainerBuilder containerBuilder)
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer(containerBuilder));
            return configuration;
        }
    }
}
