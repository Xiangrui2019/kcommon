namespace KCommon.Core.Abstract.Components
{
    // 服务组件的生命周期
    public enum LifeStyle
    {
        /// <summary>Represents a component is a transient component.
        /// </summary>
        Transient = 0,
        /// <summary>Represents a component is a scoped component.
        /// </summary>
        Scoped = 1,
        /// <summary>Represents a component is a singleton component.
        /// </summary>
        Singleton = 2
    }
}