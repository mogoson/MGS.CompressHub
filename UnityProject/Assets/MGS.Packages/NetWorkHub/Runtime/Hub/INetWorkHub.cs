/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  INetWorkHub.cs
 *  Description  :  Interface of hub to manage net works.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.Work.Net
{
    /// <summary>
    /// Interface of hub to manage net works.
    /// </summary>
    public interface INetWorkHub : IDisposable
    {
        /// <summary>
        /// Async Work hub to manage net work.
        /// </summary>
        IAsyncWorkStatusHub AsyncHub { set; get; }

        /// <summary>
        /// Get from remote async.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="headData">Head data of request.</param>
        IAsyncWorkHandler<string> GetAsync(string url, int timeout, IDictionary<string, string> headData = null);

        /// <summary>
        /// Post from remote async.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="postData">Post data of request.</param>
        /// <param name="headData">Head data of request.</param>
        IAsyncWorkHandler<string> PostAsync(string url, int timeout, string postData, IDictionary<string, string> headData = null);

        /// <summary>
        /// Download from remote async.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="filePath">Path of local file.</param>
        /// <param name="headData">Head data of request.</param>
        /// <returns></returns>
        IAsyncWorkHandler<string> DownloadAsync(string url, int timeout, string filePath, IDictionary<string, string> headData = null);
    }
}