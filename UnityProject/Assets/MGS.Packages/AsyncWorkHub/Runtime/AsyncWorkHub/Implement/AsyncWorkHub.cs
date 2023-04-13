/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWork.cs
 *  Description  :  Hub to manage works.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Threading;

namespace MGS.Work
{
    /// <summary>
    /// Hub to manage works.
    /// </summary>
    public class AsyncWorkHub : IAsyncWorkHub
    {
        /// <summary>
        /// Max count of concurrency works.
        /// </summary>
        public int Concurrency { set; get; }

        /// <summary>
        /// Resolver to check retrieable.
        /// </summary>
        public IWorkResolver Resolver { set; get; }

        /// <summary>
        /// Queue for waiting works.
        /// </summary>
        protected Queue<IAsyncWork> waitingWorks = new Queue<IAsyncWork>();

        /// <summary>
        /// List for working works.
        /// </summary>
        protected List<IAsyncWork> workingWorks = new List<IAsyncWork>();

        /// <summary>
        /// Cycle(ms) for one tick.
        /// </summary>
        protected const int TICK_CYCLE = 250;

        /// <summary>
        /// Mark is disposed?
        /// </summary>
        protected bool isDisposed;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="concurrency">Max count of concurrency works.</param>
        /// <param name="resolver">Resolver to check retrieable.</param>
        public AsyncWorkHub(int concurrency = 3, IWorkResolver resolver = null)
        {
            Concurrency = concurrency;
            Resolver = resolver;
            new Thread(Tick) { IsBackground = true }.Start();
        }

        /// <summary>
        /// Enqueue work to hub.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        public virtual IAsyncWork<T> EnqueueWork<T>(IAsyncWork<T> work)
        {
            waitingWorks.Enqueue(work);
            return work;
        }

        /// <summary>
        /// Clear cache resources.
        /// </summary>
        /// <param name="workings">Clear the working works?</param>
        /// <param name="waitings">Clear the waiting works?</param>
        public virtual void Clear(bool workings, bool waitings)
        {
            if (workings)
            {
                foreach (var work in workingWorks)
                {
                    work.AbortAsync();
                }
                workingWorks.Clear();

                if (Resolver != null)
                {
                    Resolver.Clear();
                }
            }
            if (waitings)
            {
                waitingWorks.Clear();
            }
        }

        /// <summary>
        /// Dispose all resource.
        /// </summary>
        public virtual void Dispose()
        {
            Clear(true, true);
            workingWorks = null;
            waitingWorks = null;

            if (Resolver != null)
            {
                Resolver.Dispose();
                Resolver = null;
            }
            isDisposed = true;
        }

        /// <summary>
        /// Tick loop to update.
        /// </summary>
        private void Tick()
        {
            while (!isDisposed)
            {
                TickUpdate();
                Thread.Sleep(TICK_CYCLE);
            }
        }

        /// <summary>
        /// Update to dispatch works.
        /// </summary>
        private void TickUpdate()
        {
            // Dequeue waitings to workings.
            while (waitingWorks.Count > 0 && workingWorks.Count < Concurrency)
            {
                var work = waitingWorks.Dequeue();
                if (work.IsDone)
                {
                    ClearResolver(work);
                    OnWorkIsDone(work);
                    continue;
                }

                work.ExecuteAsync();
                workingWorks.Add(work);
            }

            // Check workings.
            for (int i = 0; i < workingWorks.Count; i++)
            {
                var work = workingWorks[i];
                if (work.IsDone)
                {
                    if (work.Error != null)
                    {
                        if (CheckRetrieable(work))
                        {
                            work.ExecuteAsync();
                            continue;
                        }
                    }

                    workingWorks.RemoveAt(i);
                    ClearResolver(work);
                    OnWorkIsDone(work);
                    i--;
                }
            }
        }

        /// <summary>
        /// On work is done.
        /// </summary>
        /// <param name="work"></param>
        protected virtual void OnWorkIsDone(IAsyncWork work)
        {
            if (Resolver != null)
            {
                Resolver.Clear(work);
            }
        }

        /// <summary>
        /// Check work is retrieable?
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        protected bool CheckRetrieable(IAsyncWork work)
        {
            if (Resolver == null)
            {
                return false;
            }
            return Resolver.Retrieable(work);
        }

        /// <summary>
        /// Clear the history of work in resolver.
        /// </summary>
        /// <param name="work"></param>
        protected void ClearResolver(IAsyncWork work)
        {
            if (Resolver != null)
            {
                Resolver.Clear(work);
            }
        }
    }
}