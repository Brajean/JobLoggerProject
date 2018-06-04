using System;
using JobLoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLoggerTest
{
    [TestClass]
    public class ConsoleTest
    {        
        [TestMethod]
        public void ConsoleMessageLogIsWhite()
        {
            JobLogger.LogToConsole();
            JobLogger.EnableMessageLog();

            var message = "Log message";
            JobLogger.LogMessage(message, LogType.Message);

            Assert.AreEqual(Console.ForegroundColor, ConsoleColor.White);
        }

        [TestMethod]
        public void ConsoleWarningLogIsYellow()
        {
            JobLogger.LogToConsole();
            JobLogger.EnableWarningLog();

            var message = "Log warning";
            JobLogger.LogMessage(message, LogType.Warning);

            Assert.AreEqual(Console.ForegroundColor, ConsoleColor.Yellow);
        }

        [TestMethod]
        public void ConsoleErrorLogIsRed()
        {
            JobLogger.LogToConsole();
            JobLogger.EnableErrorLog();

            var message = "Log error";
            JobLogger.LogMessage(message, LogType.Error);

            Assert.AreEqual(Console.ForegroundColor, ConsoleColor.Red);
        }
        
        [TestMethod]
        public void IfErrorNotEnableConsoleColorIsNotAffected()
        {

            JobLogger.LogToConsole();
            JobLogger.EnableWarningLog();
            JobLogger.EnableMessageLog();
            
            var message = "Log to console";
            Console.ForegroundColor = ConsoleColor.Black;
            JobLogger.LogMessage(message, LogType.Error);

            Assert.AreEqual(Console.ForegroundColor, ConsoleColor.Red);
        }
    }
}
