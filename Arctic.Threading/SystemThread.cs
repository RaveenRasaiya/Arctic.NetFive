using Arctic.Threading.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Arctic.Threading
{
    public class SystemThread : ISystemThread
    {
        private readonly IList<Semaphore> _resources;

        public SystemThread()
        {
            _resources = new List<Semaphore>();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public bool IsRunning { get; set; }
        public IList<Semaphore> Resources => _resources;
    }
}
