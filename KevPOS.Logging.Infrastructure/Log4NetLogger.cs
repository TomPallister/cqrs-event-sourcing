using System;
using System.Globalization;
using log4net;
using log4net.Config;

namespace KevPOS.Logging.Infrastructure
{
    public class Log4NetLogger
    {
        private static bool _isConfigured;

        public static void LogEntry(Type type, string methodName, string message, LoggerLevel logLevel,
            Exception exception = null)
        {
            LogEntry(type, methodName, string.Empty, string.Empty, message, logLevel, exception);
        }

        public static void LogEntry(Type type, string methodName, string methodMessage, string message,
            LoggerLevel logLevel, Exception exception = null)
        {
            LogEntry(type, methodName, string.Empty, methodMessage, message, logLevel, exception);
        }

        public static void LogEntry(Type type, string methodName, string baseMessage, string methodMessage,
            string message, LoggerLevel logLevel, Exception exception = null)
        {
            if (!_isConfigured)
            {
                XmlConfigurator.Configure();
                ILog configLogger = LogManager.GetLogger("Config");
                configLogger.Debug("Configuration read", null);
                _isConfigured = true;
            }

            ILog logger = LogManager.GetLogger(type);

            string logMessage = string.Format(
                CultureInfo.InvariantCulture,
                "{0} :: {1}{2}{3}{4}{5}",
                methodName,
                baseMessage,
                string.IsNullOrWhiteSpace(baseMessage) ? string.Empty : " | ",
                methodMessage,
                string.IsNullOrWhiteSpace(methodMessage) ? string.Empty : " | ",
                message).Trim(new[] {':', ' '});

            switch (logLevel)
            {
                case LoggerLevel.Debug:
                    logger.DebugFormat(logMessage, exception);
                    break;
                case LoggerLevel.Info:
                    logger.Info(logMessage, exception);
                    break;
                case LoggerLevel.Warning:
                    logger.Warn(logMessage, exception);
                    break;
                case LoggerLevel.Error:
                    logger.Error(logMessage, exception);
                    break;
                case LoggerLevel.Fatal:
                    logger.Fatal(logMessage, exception);
                    break;
            }
        }
    }
}