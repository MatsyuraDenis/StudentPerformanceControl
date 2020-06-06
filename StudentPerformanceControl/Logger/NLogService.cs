using System;
using NLog;

namespace Logger
{
    public class NLogService : ILogService
    {
        #region Dependencies

        private readonly ILogger _logger;

        #endregion

        #region .ctor

        public NLogService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        #endregion

        #region Implementation

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogFatal(string message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }

        public void LogFatal(string message)
        {
            _logger.Fatal(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message, Exception exception)
        {
            _logger.Warn(exception, message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
        
        #endregion
    }
}