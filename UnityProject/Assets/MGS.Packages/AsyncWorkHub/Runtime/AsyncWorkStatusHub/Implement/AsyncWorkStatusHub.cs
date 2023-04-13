/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWorkStatusHub.cs
 *  Description  :  Hub to manage work and cache data,
 *                  and let other thread notify status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Cachers;
using System.Collections.Generic;

namespace MGS.Work
{
    /// <summary>
    /// Hub to manage work and cache data,
    /// and let other thread notify status.
    /// </summary>
    public class AsyncWorkStatusHub : AsyncWorkCacheHub, IAsyncWorkStatusHub
    {
        /// <summary>
        /// Handlers for works.
        /// </summary>
        protected Dictionary<string, IAsyncWorkHandler> handlers = new Dictionary<string, IAsyncWorkHandler>();

        /// <summary>
        /// Temp list.
        /// </summary>
        protected List<string> temps = new List<string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="resultCacher">Cacher for result.</param>
        /// <param name="workCacher">Cacher for work.</param>
        /// <param name="concurrency">Max count of concurrency works.</param>
        /// <param name="resolver">Resolver to check retrieable.</param>
        public AsyncWorkStatusHub(ICacher<object> resultCacher = null,
            ICacher<IAsyncWork> workCacher = null, int concurrency = 3, IWorkResolver resolver = null)
            : base(resultCacher, workCacher, concurrency, resolver) { }

        /// <summary>
        /// Enqueue work to hub.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public new IAsyncWorkHandler<T> EnqueueWork<T>(IAsyncWork<T> work)
        {
            var enWork = base.EnqueueWork(work);
            return GetHandler(enWork);
        }

        /// <summary>
        /// Tick update to notify status.
        /// </summary>
        public void TickStatus()
        {
            temps.AddRange(handlers.Keys);
            for (int i = 0; i < temps.Count; i++)
            {
                var key = temps[i];
                var handler = handlers[key];
                handler.NotifyStatus();
                if (!handler.Work.IsDone)
                {
                    temps.RemoveAt(i);
                    i--;
                }
            }
            foreach (var key in temps)
            {
                handlers.Remove(key);
            }
            temps.Clear();
        }

        /// <summary>
        /// Clear cache resources.
        /// </summary>
        /// <param name="workings">Clear the working works?</param>
        /// <param name="waitings">Clear the waiting works?</param>
        public override void Clear(bool workings, bool waitings)
        {
            base.Clear(workings, waitings);
            handlers.Clear();
        }

        /// <summary>
        /// Dispose all resource.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            handlers = null;
        }

        /// <summary>
        /// Get handler for work.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        protected IAsyncWorkHandler<T> GetHandler<T>(IAsyncWork<T> work)
        {
            if (handlers.ContainsKey(work.Key))
            {
                return handlers[work.Key] as IAsyncWorkHandler<T>;
            }

            var handler = new AsyncWorkHandler<T>(work);
            handlers.Add(work.Key, handler);
            return handler;
        }
    }
}