/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  NetWorkHub.cs
 *  Description  :  Hub to manage net works.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.Work.Net
{
    /// <summary>
    /// Hub to manage net works.
    /// </summary>
    public class NetWorkHub : INetWorkHub
    {
        /// <summary>
        /// Async Work hub to manage net work.
        /// </summary>
        public IAsyncWorkStatusHub AsyncHub { set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="asyncHub"></param>
        public NetWorkHub(IAsyncWorkStatusHub asyncHub)
        {
            AsyncHub = asyncHub;
        }

        /// <summary>
        /// Get from remote async.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="headData">Head data of request.</param>
        public IAsyncWorkHandler<string> GetAsync(string url, int timeout, IDictionary<string, string> headData = null)
        {
            var work = new NetGetWork(url, timeout, headData);
            return AsyncHub.EnqueueWork(work);
        }

        /// <summary>
        /// Post from remote async.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="postData">Post data of request.</param>
        /// <param name="headData">Head data of request.</param>
        public IAsyncWorkHandler<string> PostAsync(string url, int timeout, string postData, IDictionary<string, string> headData = null)
        {
            var work = new NetPostWork(url, timeout, postData, headData);
            return AsyncHub.EnqueueWork(work);
        }

        /// <summary>
        /// Download from remote async.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="filePath">Path of local file.</param>
        /// <param name="headData">Head data of request.</param>
        /// <returns></returns>
        public IAsyncWorkHandler<string> DownloadAsync(string url, int timeout, string filePath, IDictionary<string, string> headData = null)
        {
            var work = new NetFileWork(url, timeout, filePath, headData);
            return AsyncHub.EnqueueWork(work);
        }

        /// <summary>
        /// Dispose all resources.
        /// </summary>
        public virtual void Dispose()
        {
            AsyncHub.Dispose();
            AsyncHub = null;
        }
    }
}