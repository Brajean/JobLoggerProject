using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace JobLoggerProject
{

    public class JobLogger
    {
        public static bool ShouldLogToFile     = false;
        public static bool ShouldLogToConsole  = false;
        public static bool ShouldLogToDatabase = false;
        public static bool ShouldLogMessages   = false;
        public static bool ShoudLogWarnings    = false;
        public static bool ShouldLogErrors     = false;
        

        public static void LogToConsole()
        {
            JobLogger.ShouldLogToConsole = true;
        }

        public static void LogToFile()
        {
            JobLogger.ShouldLogToFile = true;
        }

        public static void LogToDatabase()
        {
            JobLogger.ShouldLogToDatabase = true;
        }

        public static void EnableMessageLog()
        {
            JobLogger.ShouldLogMessages = true;
        }

        public static void EnableWarningLog()
        {
            JobLogger.ShoudLogWarnings = true;
        }

        public static void EnableErrorLog()
        {
            JobLogger.ShouldLogErrors = true;
        }

        public static void LogMessage(string message, LogType logType) 
        {

            if (!JobLogger.ShouldLogToFile && !JobLogger.ShouldLogToConsole && !JobLogger.ShouldLogToDatabase)
            {
                throw new Exception("Invalid configuration: Must have at leat one log channel.");
            }

            if (!JobLogger.ShouldLogMessages && !JobLogger.ShoudLogWarnings && !JobLogger.ShouldLogErrors)
            {
                throw new Exception("Invalid configuration: Must choose at least one log type.");
            }

            var trimmedMessage = message.Trim();

            if (trimmedMessage == null || trimmedMessage.Length == 0)
            {
                return;
            }

            
            if (ShouldLogToConsole)
            {
                LogToConsole(message, logType);
            }

            if (ShouldLogToFile)
            {
                LogToFile(message, logType);
            }

            if (ShouldLogToDatabase)
            {
                LogToDatabase(message, logType);
            }

        }
       
        private static void LogToConsole(string message, LogType logType)
        {
            if (logType == LogType.Message && ShouldLogMessages)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (logType == LogType.Warning && ShoudLogWarnings)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            if (logType == LogType.Error && ShouldLogErrors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(DateTime.Now.ToShortDateString() + " [" + logType.ToString() + "] : " + message);
        }

        private static void LogToFile(string message, LogType logType)
        {
            var file = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile " + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";

            File.AppendAllText(file, DateTime.Now.ToShortDateString() + " [" + logType.ToString() + "] : " + message + Environment.NewLine);

        }

        private static void LogToDatabase(string message, LogType logType)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();

            SqlCommand command = new SqlCommand("Insert into Log Values('" + "[" + logType.ToString() + "] : " + message + ")");
            command.ExecuteNonQuery();

        }

    }
}