/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TimeoutCacher.cs
 *  Description  :  Cacher for data with timeout.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Cachers
{
    /// <summary>
    /// Cacher for data with timeout.
    /// </summary>
    /// <typeparam name="T">Type of cache data.</typeparam>
    public class TimeoutCacher<T> : Cacher<T>, ITimeoutCacher<T>
    {
        /// <summary>
        /// Timeout(ms).
        /// </summary>
        public int Timeout { set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxCache">Max count of caches.</param>
        /// <param name="timeout">Timeout(ms).</param>
        public TimeoutCacher(int maxCache = 100, int timeout = 1000) : base(maxCache)
        {
            Timeout = timeout;
        }

        /// <summary>
        /// Check the cache is valid?
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        protected override bool CheckCacheIsValid(Cache<T> cache)
        {
            var isValid = base.CheckCacheIsValid(cache);
            if (isValid)
            {
                var ms = (DateTime.Now - cache.stamp).TotalMilliseconds;
                isValid = ms <= Timeout;
            }
            return isValid;
        }
    }
}