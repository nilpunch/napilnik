using NUnit.Framework;

namespace Napilnik.Logger
{
    static class LoggerTests
    {
        [Test]
        public static void UseCase()
        {
            Pathfinder logToFile = new Pathfinder(new FileLogger());
            Pathfinder logToConsole = new Pathfinder(new ConsoleLogger());
            Pathfinder logToFileFriday = new Pathfinder(new FridayLogger(new FileLogger()));
            Pathfinder logToConsoleFriday = new Pathfinder(new FridayLogger(new ConsoleLogger()));
            Pathfinder logToConsoleAndLogToFileFriday =
                new Pathfinder(LoggingChain.Create(new ConsoleLogger(), new FridayLogger(new FileLogger())));
        }
    }
}