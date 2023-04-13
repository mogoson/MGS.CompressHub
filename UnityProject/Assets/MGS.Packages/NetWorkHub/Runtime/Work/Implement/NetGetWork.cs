/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  NetGetWork.cs
 *  Description  :  Net get work.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022work
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MGS.Work.Net
{
    /// <summary>
    /// Net get work.
    /// </summary>
    public class NetGetWork : NetWork<string>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="headData">Head data of request.</param>
        public NetGetWork(string url, int timeout, IDictionary<string, string> headData = null) : base(url, timeout, headData) { }

        /// <summary>
        /// Execute net request.
        /// </summary>
        /// <param name="request"></param>
        protected override void ExecuteRequest(HttpWebRequest request)
        {
            request.Method = "GET";
            base.ExecuteRequest(request);
        }

        /// <summary>
        /// Read Result from stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected override string ReadResult(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}