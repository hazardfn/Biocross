using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Biocross.Log
{
    public abstract class EventHandler
    {

        private bool alive = false;
        private Queue q = null;
        private Thread dispatch = null;
        protected string[] levelDescriptors = null;

        public EventHandler()
        {
            q = Queue.Synchronized(new Queue(1000));
           
        }

        public void start()
        {
            if (alive)
                return;

            alive = true;
            dispatch = new Thread(new ThreadStart(dispatchMessages));
            dispatch.Start();
        }

        public void shutdown()
        {
            if (!alive)
                return;

            alive = false;
            Monitor.Enter(q);
            Monitor.PulseAll(q);
            Monitor.Exit(q);
        }

        public void abort()
        {
            if (!alive)
                return;

            alive = false;
            dispatch.Abort();
        }

        protected void dispatchMessages()
        {
            while(alive)
            {
                while((q.Count != 0) && alive)
                {
                    log((LoggerMessage)q.Dequeue());
                }

                if ((alive) && (q.Count == 0))
                {
                    Monitor.Enter(q);
                    if (q.Count == 0)
                        Monitor.Wait(q);
                    Monitor.Exit(q);
                }
            }

            while(q.Count != 0)
            {
                log((LoggerMessage)q.Dequeue());
            }

            onShutdown();
            dispatch = null;
        }

        public void log(string tag, int level, string levelDescription, string message)
        {
            if (!alive)
                return;

            LoggerMessage lm = new LoggerMessage();
            lm.message = message;
            lm.tag = tag;
            lm.level = level;
            lm.levelDescription = levelDescription;
            lm.time = DateTime.Now.ToFileTime();

            q.Enqueue(lm);

            Monitor.Enter(q);
            Monitor.PulseAll(q);
            Monitor.Exit(q);
        }

        protected abstract void log(LoggerMessage message);
        protected abstract void onShutdown();


    }
}
