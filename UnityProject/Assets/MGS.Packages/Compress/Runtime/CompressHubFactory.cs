/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressHubFactory.cs
 *  Description  :  Factory for create async compress work hub.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.Work.Compress
{
    /// <summary>
    /// Factory for create async compress work hub.
    /// </summary>
    public sealed class CompressHubFactory
    {
        /// <summary>
        /// Create net hub with concurrency and cache ability.
        /// </summary>
        /// <param name="compressor">Compressor to do compress work.</param>
        /// <param name="concurrency">Max count of concurrency works.</param>
        /// <param name="retryTimes">Retry times. do not active retry ability if let retryTimes=0.</param>
        /// <param name="tolerables">Tolerable exception types can be retry. default is [WebException,TimeoutException] if let it null.</param>
        /// <param name="maxCacheCount">Max count of caches.</param>
        /// <param name="cacheTimeout">Timeout(ms)</param>
        /// <returns></returns>
        public static ICompressHub CreateHub(ICompressor compressor = null,
            int concurrency = 10,
            int retryTimes = 3, ICollection<Type> tolerables = null,
            int maxCacheCount = 100, int cacheTimeout = 5000)
        {
            var asyncHub = WorkHubFactory.CreateStatusHub(concurrency, retryTimes, tolerables, maxCacheCount, cacheTimeout);
            if (compressor == null)
            {
#if true
                compressor = new IonicCompressor();
#else
                compressor = new SharpCompressor();
#endif
            }
            return new CompressHub(asyncHub, compressor);
        }
    }
}