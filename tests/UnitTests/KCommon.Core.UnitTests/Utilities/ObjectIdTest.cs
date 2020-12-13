using System;
using KCommon.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Utilities
{
    [TestClass]
    public class ObjectIdTest
    {
        [TestMethod]
        public void TestBasic()
        {
            var objectId = new ObjectId();
            Assert.AreNotEqual(objectId, null);
            Console.WriteLine(objectId);

            var objectId2 = ObjectId.GenerateNewId();
            Assert.AreNotEqual(objectId2, null);
            Console.WriteLine(objectId2);
        }

        [TestMethod]
        public void TestCompare()
        {
            var objectId = new ObjectId();
            var objectId2 = new ObjectId();
            var objectId3 = ObjectId.GenerateNewId();
            
            Assert.AreEqual(objectId, objectId2);
            Assert.AreNotEqual(objectId2, objectId3);
        }
    }
}