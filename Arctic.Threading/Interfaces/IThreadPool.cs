using System.Collections.Generic;

namespace Arctic.Threading.Interfaces
{
    public interface IThreadPool<T> where T : ISystemThread
    {
        IList<SystemThread> CurrentThreads { get; }
        IList<SystemThread> CompletedThreads { get; }
        void Add(T thread);
        void CancelOrRemove(T thread);
        void Run(T thread);
        void RunAll();
    }
}
