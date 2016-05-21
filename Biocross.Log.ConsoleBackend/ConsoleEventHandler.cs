using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biocross.Log;

namespace Biocross.Log.ConsoleBackend
{
    public class ConsoleEventHandler : EventHandler
    {
        protected override void log(LoggerMessage message)
        {
            string time = DateTime.FromFileTime(message.time).ToString();
            string log = "[" + time + " [" + message.level + ":" + message.levelDescription + " (" + message.tag + ")] " + message.message + " ]\r\n";
            Console.WriteLine(log);
        }

        protected override void onShutdown()
        {
            // No need for any shutdown logic.
        }
    }
}
