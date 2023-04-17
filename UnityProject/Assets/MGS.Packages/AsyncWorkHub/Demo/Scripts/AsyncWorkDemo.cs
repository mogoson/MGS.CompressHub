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

using System;
using System.Threading;

namespace MGS.Work.Demo
{
    public class AsyncWorkDemo : AsyncWork<string>
    {
        public AsyncWorkDemo()
        {
            Key = Guid.NewGuid().ToString();
        }

        protected override void OnExecute()
        {
            //Simulate do something.
            Thread.Sleep(1000);
            Progress = 0.5f;
#if false
            throw new InvalidOperationException("Test exception");
#endif

            //Simulate do something.
            Thread.Sleep(1000);
            Progress = 1.0f;

            Result = "AsyncWorkDemo Result For Test";
        }
    }
}