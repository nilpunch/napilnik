using System;

namespace Napilnik.Logger
{
    class FridayLogger : ILogger
    {
        private readonly ILogger _logger;

        public FridayLogger(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            
            _logger = logger;
        }
        
        public void Log(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _logger.Log(message);
        }
    }
}