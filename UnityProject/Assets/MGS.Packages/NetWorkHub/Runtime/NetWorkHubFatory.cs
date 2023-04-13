/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  NetWorkHubHandler.cs
 *  Description  :  Factory for create async net work hub.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;

namespace MGS.Work.Net
{
    /// <summary>
    /// Factory for create async net work hub.
    /// </summary>
    public sealed class NetWorkHubFatory
    {
        /// <summary>
        /// The maximum number of concurrent connections allowed by a ServicePoint object.
        /// The default connection limit is 10 for ASP.NET hosted applications and 2 for all others.
        /// </summary>
        public static int NetConnectionLimit
        {
            set { ServicePointManager.DefaultConnectionLimit = value; }
            get { return ServicePointManager.DefaultConnectionLimit; }
        }

        /// <summary>
        /// Create net hub with concurrency and cache ability.
        /// </summary>
        /// <param name="concurrency">Max count of concurrency works.</param>
        /// <param name="retryTimes">Retry times. do not active retry ability if let retryTimes=0.</param>
        /// <param name="tolerables">Tolerable exception types can be retry. default is [WebException,TimeoutException] if let it null.</param>
        /// <param name="maxCacheCount">Max count of caches.</param>
        /// <param name="cacheTimeout">Timeout(ms)</param>
        /// <returns></returns>
        public static INetWorkHub CreateHub(int concurrency = 10,
            int retryTimes = 3, ICollection<Type> tolerables = null,
            int maxCacheCount = 100, int cacheTimeout = 5000)
        {
            var asyncHub = WorkHubFactory.CreateStatusHub(concurrency, retryTimes, tolerables, maxCacheCount, cacheTimeout);
            return new NetWorkHub(asyncHub);
        }
    }
}