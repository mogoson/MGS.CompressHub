/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWorkDemo.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Work.Demo
{
    public class TestWorkHub : MonoBehaviour
    {
        IAsyncWorkStatusHub asyncHub;
        IAsyncWorkHandler<string> handler;

        void Start()
        {
            asyncHub = WorkHubFactory.CreateStatusHub();

            var work = new AsyncWorkDemo();
            handler = asyncHub.EnqueueWork(work);

            handler.OnProgressChanged += p => { Debug.LogFormat($"Progress: {p}"); };
            handler.OnCompleted += (r, e) =>
            {
                if (e == null)
                {
                    Debug.LogFormat($"Result: {r}");
                }
                else
                {
                    Debug.LogErrorFormat($"Error: {e.Message}\r\n{e.StackTrace}");
                }
            };
        }

        void OnDestroy()
        {
            handler.Dispose();
            handler = null;

            asyncHub.Dispose();
            asyncHub = null;
        }
    }
}