namespace KCommon.Core.Abstract.Components
{
    // 服务组件的生命周期
    public enum LifeStyle
    {
        /// <summary>Represents a component is a transient component.
        /// </summary>
        Transient,
        /// <summary>Represents a component is a scoped component.
        /// </summary>
        Scoped,
        /// <summary>Represents a component is a singleton component.
        /// </summary>
        Singleton
    }
}