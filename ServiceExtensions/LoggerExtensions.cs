using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace ServiceExtensions
{
    public static class LoggerExtensions
    {
        public static void LogAppError(this ILogger logger, Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            using var prop = LogContext.PushProperty("Method", memberName);
            LogContext.PushProperty("FilePath", sourceFilePath);
            LogContext.PushProperty("LineNumber", sourceLineNumber);
            logger.LogError(exception, message);
        }

        public static void LogAppWarning(this ILogger logger, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            using var prop = LogContext.PushProperty("Method", memberName);
            LogContext.PushProperty("FilePath", sourceFilePath);
            LogContext.PushProperty("LineNumber", sourceLineNumber);
            logger.LogWarning(message);
        }

        public static void LogAppInfo(this ILogger logger, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, params object[] args)
        {
            //using var prop = LogContext.PushProperty("Method", memberName);
            LogContext.PushProperty("FilePath", sourceFilePath);
            LogContext.PushProperty("LineNumber", sourceLineNumber);
            logger.LogInformation(message, args);
        }

    }
}
