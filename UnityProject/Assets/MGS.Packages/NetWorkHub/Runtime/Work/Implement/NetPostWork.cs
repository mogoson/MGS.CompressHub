/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  NetPostWork.cs
 *  Description  :  Net post work.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MGS.Work.Net
{
    /// <summary>
    /// Net post work.
    /// </summary>
    public class NetPostWork : NetWork<string>
    {
        /// <summary>
        /// Post data of request.
        /// </summary>
        public string PostData { protected set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">Remote url string.</param>
        /// <param name="timeout">Timeout(ms) of request.</param>
        /// <param name="postData">Post data of request.</param>
        /// <param name="headData">Head data of request.</param>
        public NetPostWork(string url, int timeout, string postData, IDictionary<string, string> headData = null) : base(url, timeout, headData)
        {
            Key = $"{url}{postData}".GetHashCode().ToString();
            PostData = postData;
        }

        /// <summary>
        /// Execute net request.
        /// </summary>
        /// <param name="request"></param>
        protected override void ExecuteRequest(HttpWebRequest request)
        {
            request.Method = "POST";
            using (var requestStream = request.GetRequestStream())
            {
                var postBuffer = Encoding.UTF8.GetBytes(PostData);
                requestStream.Write(postBuffer, 0, postBuffer.Length);
            }
            Progress = 0.5f;

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