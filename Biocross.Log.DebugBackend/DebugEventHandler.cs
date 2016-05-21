using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Biocross.Log.DebugBackend
{
    public class DebugEventHandler : EventHandler
    {
        protected override void log(LoggerMessage message)
        {
            string time = DateTime.FromFileTime(message.time).ToString();
            string log = "[" + time + " [" + message.level + ":" + message.levelDescription + " (" + message.tag + ")] " + message.message + " ]\r\n";
            Trace.WriteLine(log);
            Debug.WriteLine(log);
        }

        protected override void onShutdown()
        {
            // No need for any shutdown logic.
        }
    }
}

