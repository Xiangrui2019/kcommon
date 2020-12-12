using System;
using System.ComponentModel.DataAnnotations;
using KCommon.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Utilities
{
    [TestClass]
    public class ValidationHelperTest
    {
        [TestMethod]
        public void TestValid()
        {
            var v = new V
            {
                A = 5
            };

            var modelState = ValidationHelper.Ensure(v);
            Assert.AreEqual(modelState.IsValid, true);

            v = new V
            {
                A = 300,
            };
            
            modelState = ValidationHelper.Ensure(v);
            Assert.AreEqual(modelState.IsValid, false);
        }
        
        [TestMethod]
        public void TestValidThrow()
        {
            var v = new V
            {
                A = 5
            };

            try
            {
                ValidationHelper.EnsureAndThrow(v);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            
            v = new V
            {
                A = 3600
            };

            try
            {
                ValidationHelper.EnsureAndThrow(v);
            }
            catch
            {
                // ignored
            }
        }
    }

    internal class V
    {
        [Range(1, 10)]
        public int A { get; set; }
    }
}