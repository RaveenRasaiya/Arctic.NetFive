using Arctic.Threading.Extensions;
using Arctic.Threading.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Arctic.Threading
{
    public class ThreadPool : IThreadPool<SystemThread>
    {
        private readonly IList<SystemThread> _currentThreads;
        private readonly IList<SystemThread> _completedThreads;
        public ThreadPool()
        {
            _currentThreads = new List<SystemThread>();
            _completedThreads = new List<SystemThread>();
        }

        public IList<SystemThread> CurrentThreads => _currentThreads;

        public IList<SystemThread> CompletedThreads => _completedThreads;

        public void Add(SystemThread thread)
        {
            CurrentThreads.Add(thread);
        }

        public void CancelOrRemove(SystemThread thread)
        {
            CurrentThreads.Remove(thread);
        }

        public void Run(SystemThread thread)
        {
            var _thread = new Thread(BaseRun(thread));
            _thread.Start();
            Thread.Sleep(100);
        }

        public void RunAll()
        {
            foreach (var _thread in CurrentThreads)
            {
                Run(_thread);
            }
        }

        private ParameterizedThreadStart BaseRun(SystemThread thread)
        {
            return _ =>
            {
                thread.Resources.WaitAll();
                Thread.Sleep(new Random().Next(100, 1000));
                CompletedThreads.Add(thread);
                thread.Resources.ReleaseAll();
            };
        }
    }
}
