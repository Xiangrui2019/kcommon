using KCommon.Core.Abstract.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Components
{
    [TestClass]
    public class LifeStyleTest
    {
        [TestMethod]
        public void TestBasic()
        {
            var life = LifeStyle.Transient;
            
            Assert.AreEqual(life, LifeStyle.Transient);
        }

        [TestMethod]
        public void TestToString()
        {
            var life = LifeStyle.Transient;
            
            Assert.AreEqual(life.ToString(), "Transient");
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            var life = LifeStyle.Transient;
            var life_eq = LifeStyle.Transient;
            
            Assert.AreEqual(life.GetHashCode(), life_eq.GetHashCode());
        }
        
        [TestMethod]
        public void TestGetHashCodeNotEq()
        {
            var life = LifeStyle.Transient;
            var life_eq = LifeStyle.Scoped;
            
            Assert.AreNotEqual(life.GetHashCode(), life_eq.GetHashCode());
        }
    }
}