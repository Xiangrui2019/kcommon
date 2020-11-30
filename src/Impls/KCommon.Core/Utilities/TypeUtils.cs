using KCommon.Core.Abstract.Components;
using System;
using System.ComponentModel;
using System.Linq;

namespace KCommon.Core.Utilities
{
    /// <summary>A class provides utility methods.
    /// </summary>
    public static class TypeUtils
    {
        /// <summary>Convert the given object to a given strong type.
        /// </summary>
        public static T ConvertType<T>(object value)
        {
            if (value == null)
            {
                return default(T);
            }

            var typeConverter1 = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter1.CanConvertFrom(value.GetType()))
            {
                return (T)typeConverter1.ConvertFrom(value);
            }

            var typeConverter2 = TypeDescriptor.GetConverter(value.GetType());
            if (typeConverter2.CanConvertTo(typeof(T)))
            {
                return (T)typeConverter2.ConvertTo(value, typeof(T));
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>Check whether a type is a component type.
        /// </summary>
        public static bool IsComponent(Type type)
        {
            return type.IsClass && !type.IsAbstract && type.GetCustomAttributes(typeof(ComponentAttribute), false).Any();
        }
    }
}