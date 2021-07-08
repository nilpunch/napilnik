namespace Napilnik.Logger
{
    class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            _logger.Log("Finding something...");
        }
    }
}