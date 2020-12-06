using System;

namespace KCommon.Core.Abstract.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>The lifetime of the component.
        /// </summary>
        public LifeStyle LifeStyle { get; private set; }

        /// <summary>Default constructor.
        /// </summary>
        public ComponentAttribute() : this(LifeStyle.Singleton)
        {
        }

        /// <summary>Parameterized constructor.
        /// </summary>
        /// <param name="lifeStyle"></param>
        public ComponentAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }
    }
}