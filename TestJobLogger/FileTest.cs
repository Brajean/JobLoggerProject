using System;
using System.Configuration;
using System.IO;
using JobLoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLoggerTest
{
    [TestClass]
    public class FileTest
    {
        [TestMethod]
        public void FileMessageLog()
        {
            JobLogger.LogToFile();
            JobLogger.EnableMessageLog();

            var message = "Test message";
            var messageFile = DateTime.Now.ToShortDateString() + " [" + LogType.Message.ToString() + "] : " + message;

            JobLogger.LogMessage(message, LogType.Message);

            var file = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile " + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            var lines = File.ReadAllLines(file);
            var lastLine = lines[lines.Length - 1];

            Assert.AreEqual(messageFile, lastLine);
        }
        
        [TestMethod]
        public void FileWarningLog()
        {
            JobLogger.LogToFile();
            JobLogger.EnableMessageLog();

            var message = "Test warning";
            var messageFile = DateTime.Now.ToShortDateString() + " [" + LogType.Warning.ToString() + "] : " + message;

            JobLogger.LogMessage(message, LogType.Warning);

            var file = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile " + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            var lines = File.ReadAllLines(file);
            var lastLine = lines[lines.Length - 1];

            Assert.AreEqual(messageFile, lastLine);
        }

        [TestMethod]
        public void FileErrorLog()
        {
            JobLogger.LogToFile();
            JobLogger.EnableMessageLog();

            var message = "Test error";
            var messageFile = DateTime.Now.ToShortDateString() + " [" + LogType.Error.ToString() + "] : " + message;

            JobLogger.LogMessage(message, LogType.Error);

            var file = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile " + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            var lines = File.ReadAllLines(file);
            var lastLine = lines[lines.Length - 1];

            Assert.AreEqual(messageFile, lastLine);
        }
    }
}
