/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICacher.cs
 *  Description  :  Interface of cacher.
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
    /// Interface of cacher.
    /// </summary>
    /// <typeparam name="T">Type of cache data.</typeparam>
    public interface ICacher<T> : IDisposable
    {
        /// <summary>
        /// Max count of caches.
        /// </summary>
        int MaxCache { set; get; }

        /// <summary>
        /// Count of current cache.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Set cache data.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, T value);

        /// <summary>
        /// Get cache data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get(string key);

        /// <summary>
        /// Remove cache data.
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// Clear all caches.
        /// </summary>
        void Clear();
    }
}