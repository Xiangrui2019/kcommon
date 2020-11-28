using KCommon.Core.Abstract.Models;

namespace KCommon.Core.Abstract.Components
{
    public class LifeStyle : Enumeration
    {
        public LifeStyle(int id, string name) : base(id, name)
        {
        }
        
        // 每次重新创建对象的组件
        public static readonly LifeStyle Transient = new LifeStyle(1, "Transient");
        // 单例模式的组件
        public static readonly LifeStyle Singleton = new LifeStyle(2, "Singleton");
    }
}