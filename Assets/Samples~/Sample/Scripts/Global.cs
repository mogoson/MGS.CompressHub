/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Global.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/27/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Operate;

namespace MGS.Compress.Sample
{
    public sealed class Global
    {
        public static ICompressHub CompressHub { get; }

        static Global()
        {
            var asyncHub = new AsyncOperateHub();       //Execute immediately
            //var asyncHub = new AsyncOperateHubPro();  //Concurrent scheduling
            CompressHub = new CompressHub(asyncHub);
        }
    }
}