using System;
using System.IO;

namespace Napilnik.Logger
{
    class FileLogger : ILogger
    {
        public void Log(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            
            File.WriteAllText("log.txt", message);
        }
    }
}