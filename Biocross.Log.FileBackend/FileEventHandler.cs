using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Biocross.Log;

namespace Biocross.Log.FileBackend
{
    public class FileEventHandler : EventHandler
    {
        StreamWriter stream = null;
        bool append = true;

        public FileEventHandler(String filename)
        {
            if (filename != null)
            {
                FileMode fm;

                if (append) fm = FileMode.Append;
                else fm = FileMode.Create;

                FileStream fs = new FileStream(filename, fm, FileAccess.Write, FileShare.Read);

                //file = new StreamWriter(filename);

                stream = new StreamWriter(fs, System.Text.Encoding.UTF8, 4096);
                //stream = new StreamWriter(fs, System.Text.Encoding.UTF8, 1024);
            }
        }

        override protected void log(LoggerMessage message)
        {
            if (stream == null) return;
            string time = System.DateTime.FromFileTime(message.time).ToString();
            string test = "[" + time + " [" + message.level + ":" + message.levelDescription + " (" + message.tag + ")] " + message.message + " ]\r\n";
            stream.Write("[" + time + " [" + message.level + ":" + message.levelDescription + " (" + message.tag + ")] " + message.message + " ]\r\n");
            stream.Flush();
        }

        protected override void onShutdown()
        {
            if (stream != null)
            {
                stream.Flush();
                stream.Close();
            }
        }

        /// <summary>
        /// Flag to append the text file.  If this flag is not set it will overwrite.
        /// </summary>
        /// <param name="flag"></param>
        public void setAppend(bool flag)
        {
            this.append = flag;
        }

        /// <summary>
        /// Get's append flag
        /// </summary>
        /// <returns></returns>
        public bool getAppend()
        {
            return append;
        }
    }
}
