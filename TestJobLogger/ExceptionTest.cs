using System;
using JobLoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLoggerTest
{
    [TestClass]
    public class ExceptionTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ThrowErrorIfJobLoggerNotConfiguredProperly()
        {
            JobLogger.LogMessage("Hello World", LogType.Message);
            Assert.Fail();
        }
    }
}
