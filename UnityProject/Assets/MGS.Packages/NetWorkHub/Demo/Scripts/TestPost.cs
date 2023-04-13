/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestPost.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

#define LISTEN_NOTIFY

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Work.Net.Demo
{
    public class TestPost : TestNet
    {
        public string body;

        protected override void Start()
        {
            base.Start();

            var header = new Dictionary<string, string>() { { "Content-Type", "application/json" } };
            handler = hub.PostAsync(url, 120000, body, header);

#if LISTEN_NOTIFY
            handler.OnProgressChanged += progress => Debug.Log($"progress: {progress.ToString("f3")}");
            handler.OnCompleted += (result, error) =>
            {
                var msg = error == null ? "" : error.Message;
                Debug.Log($"result: {result}, error: {msg}");
            };
#endif
        }

#if !LISTEN_NOTIFY
        void Update()
        {
            if (!handler.Work.IsDone)
            {
                Debug.LogFormat("progress is {0}", handler.Work.Progress.ToString("f3"));
            }
            else
            {
                if (handler.Work.Error == null)
                {
                    Debug.LogFormat("result is {0}", handler.Work.Result);
                }
                else
                {
                    Debug.LogErrorFormat("error is {0}", handler.Work.Error.Message);
                }
                enabled = false;
            }
        }
#endif
    }
}