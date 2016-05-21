using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.IO;
using System.Linq;
using System.Text;

namespace Biocross.Log
{
    public struct LoggerMessage
    {
        public int level;
        public string levelDescription;
        public string tag;
        public string message;
        public long time;
    }

    public class Logger
    {
        private static Logger logger;
        protected static string[] logLevelDesc = null;

        protected EventHandler[][] eh;
        protected uint max = 0;
        protected uint levels = 0;
        protected EventHandler defaultHandler = null;

        public enum LogLevels
        {
            CRITICAL=0,
            ERROR=1,
            WARN=2,
            INFO=3,
            DEBUG=4,
            ALL=5
        }

        public Logger(EventHandler defaultHandler)
        {
            init(defaultHandler);
        }

        public Logger()
        {
            init(null);
        }

        public Logger promoteToStatic()
        {
            logger = this;
            return logger;
        }

        private void init(EventHandler defaultHandler)
        {
            this.levels = 6;
            this.defaultHandler = defaultHandler;
            this.max = levels - 1;

            eh = new EventHandler[levels][];

            EventHandler[] handler = new EventHandler[1];

            handler[0] = defaultHandler;

            for (int i = 0; i < levels; i++)
            {
                eh[i] = handler;
            }

            logLevelDesc = new string[6];
            logLevelDesc[0] = "V_CRITICAL";
            logLevelDesc[1] = "V_ERROR";
            logLevelDesc[2] = "V_WARN";
            logLevelDesc[3] = "V_INFO";
            logLevelDesc[4] = "V_DEBUG";
            logLevelDesc[5] = "V_ALL";
        }

        public void setMaximumLogLevel(uint max)
        {
            this.max = max;
        }

        public uint getMaximumLogLevel()
        {
            return max;
        }

        public EventHandler getDefaultLoggerEventHandler()
        {
            return this.defaultHandler;
        }

        public void addSpecialLoggerToAllLevels(EventHandler handler)
        {
            if (handler == null) return;

            for (int level = 0; level < this.levels; level++)
            {
                addSpecialLogger(level, handler);
            }
        }

        public void addSpecialLogger(int level, EventHandler handler)
        {
            if (level < levels)
            {
                if (eh[level] != null)
                {
                    int size = eh[level].Length + 1;
                    EventHandler[] temp = new EventHandler[size];

                    for (int i = 0; i < eh[level].Length; i++)
                    {
                        temp[i] = eh[level][i];
                    }

                    temp[size - 1] = handler;

                    eh[level] = temp;
                }
                else
                {
                    eh[level] = new EventHandler[1];
                    eh[level][0] = handler;
                }
            }
        }

        public void log(int level, string tag, string message)
        {
            if ((level <= max) && (level < levels) && (eh[level] != null))
            {
                for (int i = 0; i < eh[level].Length; i++)
                {
                    if (logLevelDesc == null)
                    {
                        if (eh[level][i] != null) eh[level][i].log(tag, level, "", message);
                    }
                    else
                    {
                        if (eh[level][i] != null) eh[level][i].log(tag, level, logLevelDesc[level], message);
                    }
                }
            }
        }

        public void shutdown()
        {
            for (int level = 0; level < eh.Length; level++)
            {
                for (int i = 0; i < eh[level].Length; i++)
                {
                    if (eh[level][i] != null) eh[level][i].shutdown();
                }
            }
        }
    }
}
