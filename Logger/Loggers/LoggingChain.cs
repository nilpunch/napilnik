using System.Collections.Generic;

namespace Napilnik.Logger
{
    public class LoggingChain : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        private LoggingChain(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void Log(string message)
        {
            foreach (var logger in _loggers)
                logger.Log(message);
        }

        public static LoggingChain Create(params ILogger[] loggers)
        {
            return new LoggingChain(loggers);
        }
    }
}