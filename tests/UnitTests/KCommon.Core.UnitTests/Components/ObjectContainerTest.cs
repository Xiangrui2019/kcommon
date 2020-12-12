using KCommon.Core.Components;
using KCommon.Core.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Components
{
    [TestClass]
    public class ObjectContainerTest
    {
        [TestMethod]
        public void TestBasic()
        {
            ObjectContainer.SetContainer(new MockObjectContainer());
            ObjectContainer.Build();
        }
    }
}