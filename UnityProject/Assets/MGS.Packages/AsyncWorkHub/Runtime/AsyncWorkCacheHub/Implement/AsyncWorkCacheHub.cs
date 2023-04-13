/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWorkCacheHub.cs
 *  Description  :  Hub to manage works and cache data.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Cachers;

namespace MGS.Work
{
    /// <summary>
    /// Hub to manage works and cache data.
    /// </summary>
    public class AsyncWorkCacheHub : AsyncWorkHub, IAsyncWorkCacheHub
    {
        /// <summary>
        /// Cacher for result.
        /// </summary>
        public ICacher<object> ResultCacher { set; get; }

        /// <summary>
        /// Cacher for works.
        /// </summary>
        public ICacher<IAsyncWork> WorksCacher { set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="resultCacher">Cacher for result.</param>
        /// <param name="workCacher">Cacher for work.</param>
        /// <param name="concurrency">Max count of concurrency works.</param>
        /// <param name="resolver">Resolver to check work retrieable.</param>
        public AsyncWorkCacheHub(ICacher<object> resultCacher = null, ICacher<IAsyncWork> workCacher = null,
            int concurrency = 3, IWorkResolver resolver = null) : base(concurrency, resolver)
        {
            ResultCacher = resultCacher;
            WorksCacher = workCacher;
        }

        /// <summary>
        /// Enqueue work to hub.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        public override IAsyncWork<T> EnqueueWork<T>(IAsyncWork<T> work)
        {
            var enWork = GetCacheAsWork<T>(work.Key);
            if (enWork == null)
            {
                enWork = base.EnqueueWork(work);
                SetCacheWork(enWork.Key, enWork);
            }
            return enWork;
        }

        /// <summary>
        /// Clear cache resources.
        /// </summary>
        /// <param name="workings">Clear the working works?</param>
        /// <param name="waitings">Clear the waiting works?</param>
        public override void Clear(bool workings, bool waitings)
        {
            base.Clear(workings, waitings);

            ResultCacher.Clear();
            WorksCacher.Clear();
        }

        /// <summary>
        /// Dispose all resource.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            ResultCacher.Dispose();
            ResultCacher = null;

            WorksCacher.Dispose();
            WorksCacher = null;
        }

        /// <summary>
        /// On work is done.
        /// </summary>
        /// <param name="work"></param>
        protected override void OnWorkIsDone(IAsyncWork work)
        {
            if (work.Result != null)
            {
                SetCacheResult(work.Key, work.Result);
            }

            RemoveCacheWork(work.Key);
            base.OnWorkIsDone(work);
        }

        /// <summary>
        /// Get work from one of the cachers(ResultCacher/WorksCacher).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected IAsyncWork<T> GetCacheAsWork<T>(string key)
        {
            var result = GetCacheResult(key);
            if (result is T resultT)
            {
                return new CacheWork<T>(resultT);
            }

            return GetCacheWork(key) as IAsyncWork<T>;
        }

        /// <summary>
        /// Get result from ResultCacher.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected object GetCacheResult(string key)
        {
            if (ResultCacher == null)
            {
                return null;
            }
            return ResultCacher.Get(key);
        }

        /// <summary>
        /// Set result to ResultCacher.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="result"></param>
        protected void SetCacheResult(string key, object result)
        {
            if (ResultCacher != null)
            {
                ResultCacher.Set(key, result);
            }
        }

        /// <summary>
        /// Get work from WorkCacher.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected IAsyncWork GetCacheWork(string key)
        {
            if (WorksCacher == null)
            {
                return null;
            }
            return WorksCacher.Get(key);
        }

        /// <summary>
        /// Set work to WorkCacher.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="work"></param>
        protected void SetCacheWork(string key, IAsyncWork work)
        {
            if (WorksCacher != null)
            {
                WorksCacher.Set(key, work);
            }
        }

        /// <summary>
        /// Remove work from WorkCacher.
        /// </summary>
        /// <param name="key"></param>
        protected void RemoveCacheWork(string key)
        {
            if (WorksCacher != null)
            {
                WorksCacher.Remove(key);
            }
        }
    }
}