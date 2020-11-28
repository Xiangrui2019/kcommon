using KCommon.Core.Components;
using KCommon.Core.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Configurations
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void TestCreate()
        {
            Configuration.Create();
        }
    }
}