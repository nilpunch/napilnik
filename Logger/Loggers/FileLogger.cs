using System.IO;

namespace Napilnik.Logger
{
    class FileLogger : ILogger
    {
        public void Log(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }
}