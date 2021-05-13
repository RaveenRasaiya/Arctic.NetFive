using Arctic.Threading;
using Arctic.Threading.Extensions;
using Arctic.Threading.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Arctic.Tests
{
    public class ThreadingTests
    {
        // a few thread resources

        private readonly Semaphore _resourceA = new Semaphore(1, 1);
        private readonly Semaphore _resourceB = new Semaphore(1, 1);
        private readonly Semaphore _resourceC = new Semaphore(1, 1);
        private readonly IThreadPool<SystemThread> _threadPool;
        public ThreadingTests()
        {
            _threadPool = new Threading.ThreadPool();
            SystemThread _systemThread = new SystemThread
            {
                Name = "Process 1",
                Id = Guid.NewGuid()
            };
            _systemThread.Resources.Add(_resourceA);
            _threadPool.Add(_systemThread);

            _systemThread = new SystemThread
            {
                Name = "Process 2",
                Id = Guid.NewGuid()
            };
            _systemThread.Resources.Add(_resourceB);
            _threadPool.Add(_systemThread);

            _systemThread = new SystemThread
            {
                Name = "Process 3",
                Id = Guid.NewGuid()
            };
            _systemThread.Resources.Add(_resourceA);
            _systemThread.Resources.Add(_resourceB);
            _threadPool.Add(_systemThread);

            _systemThread = new SystemThread
            {
                Name = "Process 4",
                Id = Guid.NewGuid()
            };
            _systemThread.Resources.Add(_resourceC);
            _threadPool.Add(_systemThread);

            _systemThread = new SystemThread
            {
                Name = "Process 5",
                Id = Guid.NewGuid()
            };
            _systemThread.Resources.Add(_resourceB);
            _threadPool.Add(_systemThread);

            _threadPool.RunAll();

            Thread.Sleep(6000);
        }


        [Fact]
        public void ThreadPool_First_Process()
        {
            var firstProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 1");
            firstProcessIndex.Should().BeLessOrEqualTo(2);
        }

        [Fact]
        public void ThreadPool_First_Process_Negative_Case()
        {
            var firstProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 1");
            firstProcessIndex.Should().NotBeInRange(4, 5);
        }

        [Fact]
        public void ThreadPool_Second_Process()
        {
            var firstProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 2");
            firstProcessIndex.Should().BeLessOrEqualTo(2);
        }

        [Fact]
        public void ThreadPool_Fourth_Process()
        {
            var firstProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 4");
            firstProcessIndex.Should().BeLessOrEqualTo(2);
        }

        [Fact]
        public void ThreadPool_Process_Three_WaitFor_Process_One_And_Two()
        {
            var firstProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 1");
            var secondProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 2");
            var thirdProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 3");
            firstProcessIndex.Should().BeLessThan(thirdProcessIndex);
            secondProcessIndex.Should().BeLessThan(thirdProcessIndex);
        }


        [Fact]
        public void ThreadPool_Process_Five_WaitFor_Process_Two_And_Three()
        {
            var secondProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 2");
            var thirdProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 3");
            var fifthProcessIndex = _threadPool.CompletedThreads.FindIndex(0, x => x.Name == "Process 5");
            secondProcessIndex.Should().BeLessThan(fifthProcessIndex);
            secondProcessIndex.Should().BeLessThan(fifthProcessIndex);
        }

    }
}
