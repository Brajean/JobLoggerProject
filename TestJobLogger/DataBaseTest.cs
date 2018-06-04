using System;
using System.Configuration;
using System.Data.SqlClient;
using JobLoggerProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLoggerTest
{
    [TestClass]
    public class DataBaseTest
    {
        [TestMethod]
        public void DatabaseMessageLog()
        {
            JobLogger.LogToDatabase();
            JobLogger.EnableMessageLog();

            var message = "Test message";
            var statement = "[" + LogType.Message.ToString() + "] : " + message;

            JobLogger.LogMessage(message, LogType.Message);

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT LogDescription FROM dbo.Log ORDER BY Id DESC LIMIT 1;");
            var lastRecord = "";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lastRecord = reader[0].ToString();
                }
            }

            Assert.AreEqual(statement, lastRecord);
        }

        [TestMethod]
        public void DatabaseWarningLog()
        {
            JobLogger.LogToDatabase();
            JobLogger.EnableMessageLog();

            var message = "Test warning";
            var statement = "[" + LogType.Warning.ToString() + "] : " + message;

            JobLogger.LogMessage(message, LogType.Warning);

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT LogDescription FROM dbo.Log ORDER BY Id DESC LIMIT 1;");
            var lastRecord = "";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lastRecord = reader[0].ToString();
                }
            }

            Assert.AreEqual(statement, lastRecord);
        }

        [TestMethod]
        public void DatabaseErrorLog()
        {
            JobLogger.LogToDatabase();
            JobLogger.EnableMessageLog();

            var message = "Test error";
            var statement = "[" + LogType.Error.ToString() + "] : " + message;

            JobLogger.LogMessage(message, LogType.Error);

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT LogDescription FROM dbo.Log ORDER BY Id DESC LIMIT 1;");
            var lastRecord = "";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lastRecord = reader[0].ToString();
                }
            }

            Assert.AreEqual(statement, lastRecord);
        }
    }
}
