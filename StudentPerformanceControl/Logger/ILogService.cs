using System;

namespace Logger
{
    public interface ILogService
    {
        void LogDebug(string message);
        void LogError(string message, Exception exception);
        void LogError(string message);
        void LogFatal(string message, Exception exception);
        void LogFatal(string message);
        void LogInfo(string message);
        void LogWarn(string message, Exception exception);
        void LogWarn(string message);
    }
}