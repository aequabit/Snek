using System;
using System.Threading;

namespace Snek.Core
{
    public class Worker
    {
        /// <summary>
        /// Thread shutdown event.
        /// </summary>
        private readonly ManualResetEvent _shutdownEvent = new ManualResetEvent(false);

        /// <summary>
        /// Thread pause event.
        /// </summary>
        private readonly ManualResetEvent _pauseEvent = new ManualResetEvent(true);

        /// <summary>
        /// Worker action.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// Worker thread.
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// Worker state.
        /// </summary>
        private bool _running;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="action">Action of for the worker.</param>
        public Worker(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// Starts the worker.
        /// </summary>
        public void Start()
        {
            _running = true;
            _thread = new Thread(_worker);
            _thread.Start();
        }

        /// <summary>
        /// Pauses the worker.
        /// </summary>
        public void Pause()
        {
            _pauseEvent.Reset();
        }

        /// <summary>
        /// Resumes the worker.
        /// </summary>
        public void Resume()
        {
            _pauseEvent.Set();
        }

        /// <summary>
        /// Stops the worker.
        /// </summary>
        public void Stop()
        {
            _running = false;

            _shutdownEvent.Set();

            _pauseEvent.Set();

            _thread.Join();
        }

        /// <summary>
        /// Worker thread loop.
        /// </summary>
        private void _worker()
        {
            while (_running)
            {
                _pauseEvent.WaitOne(Timeout.Infinite);

                if (_shutdownEvent.WaitOne(0))
                    break;

                _action();
            }
        }
    }
}