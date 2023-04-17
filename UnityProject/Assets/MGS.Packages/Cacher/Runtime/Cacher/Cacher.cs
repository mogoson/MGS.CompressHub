/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Cacher.cs
 *  Description  :  Cacher for cache data.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.Cachers
{
    /// <summary>
    /// Cacher for cache data.
    /// </summary>
    /// <typeparam name="T">Type of cache data.</typeparam>
    public class Cacher<T> : ICacher<T>
    {
        /// <summary>
        /// Max count of caches.
        /// </summary>
        public int MaxCache { set; get; }

        /// <summary>
        /// Count of current cache.
        /// </summary>
        public int Count { get { return caches.Count; } }

        /// <summary>
        /// Cache datas.
        /// </summary>
        protected Dictionary<string, Cache<T>> caches = new Dictionary<string, Cache<T>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxCache">Max count of caches.</param>
        public Cacher(int maxCache = 100)
        {
            MaxCache = maxCache;
        }

        /// <summary>
        /// Set cache data.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, T value)
        {
            var keys = caches.Keys.GetEnumerator();
            while (caches.Count >= MaxCache)
            {
                keys.MoveNext();
                caches.Remove(keys.Current);
            }

            var cache = new Cache<T>(value);
            caches[key] = cache;
        }

        /// <summary>
        /// Get cache data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            if (caches.ContainsKey(key))
            {
                var cache = caches[key];
                if (CheckCacheIsValid(cache))
                {
                    return cache.content;
                }
                Remove(key);
            }
            return default(T);
        }

        /// <summary>
        /// Remove cache data.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            caches.Remove(key);
        }

        /// <summary>
        /// Clear all caches.
        /// </summary>
        public void Clear()
        {
            caches.Clear();
        }

        /// <summary>
        /// Dispose all resources.
        /// </summary>
        public void Dispose()
        {
            Clear();
            caches = null;
        }

        /// <summary>
        /// Check the cache is valid?
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        protected virtual bool CheckCacheIsValid(Cache<T> cache)
        {
            return true;
        }
    }
}