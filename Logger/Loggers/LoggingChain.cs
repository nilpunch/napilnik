using System;
using System.Collections.Generic;

namespace Napilnik.Logger
{
    public class LoggingChain : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        private LoggingChain(IEnumerable<ILogger> loggers)
        {
            if (loggers == null)
                throw new ArgumentNullException(nameof(loggers));
            
            _loggers = loggers;
        }

        public void Log(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            
            foreach (var logger in _loggers)
                logger.Log(message);
        }

        public static LoggingChain Create(params ILogger[] loggers)
        {
            if (loggers == null)
                throw new ArgumentNullException(nameof(loggers));
            
            return new LoggingChain(loggers);
        }
    }
}