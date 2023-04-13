/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestNet.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Work.Net.Demo
{
    public class TestNet : MonoBehaviour
    {
        public string url;
        protected INetWorkHub hub;
        protected IAsyncWorkHandler<string> handler;

        protected virtual void Start()
        {
            hub = NetWorkHubFatory.CreateHub();
        }

        protected virtual void OnDestroy()
        {
            handler.Dispose();
            handler = null;

            hub.Dispose();
            hub = null;
        }
    }
}