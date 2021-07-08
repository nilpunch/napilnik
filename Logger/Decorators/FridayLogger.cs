using System;

namespace Napilnik.Logger
{
    class FridayLogger : ILogger
    {
        private readonly ILogger _logger;

        public FridayLogger(ILogger logger)
        {
            _logger = logger;
        }
        
        public void Log(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _logger.Log(message);
        }
    }
}