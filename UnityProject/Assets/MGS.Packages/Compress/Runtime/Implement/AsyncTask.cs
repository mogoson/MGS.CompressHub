/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncTask.cs
 *  Description  :  Async compress task.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;

namespace MGS.Compress
{
    /// <summary>
    /// Async compress task.
    /// </summary>
    internal abstract class AsyncTask : ITask
    {
        /// <summary>
        /// State of compressor task.
        /// </summary>
        public TaskState State { protected set; get; }

        /// <summary>
        /// Entries associated with the task.
        /// </summary>
        public abstract IEnumerable<string> Entries { get; }

        protected ICompressor compressor = null;
        protected bool clearBefor = true;
        protected Action<float> progressCallback = null;
        protected Action<bool, string, Exception> finishedCallback = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressor"></param>
        /// <param name="clearBefor"></param>
        /// <param name="progressCallback"></param>
        /// <param name="finishedCallback"></param>
        public AsyncTask(ICompressor compressor, bool clearBefor = true,
               Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null)
        {
            this.compressor = compressor;
            this.clearBefor = clearBefor;
            this.progressCallback = progressCallback;
            this.finishedCallback = finishedCallback;
        }

        /// <summary>
        /// Start compressor task.
        /// </summary>
        public void Start()
        {
            if (State == TaskState.Working)
            {
                return;
            }

            new Thread(() =>
            {
                Execute();
                State = TaskState.Finished;
            })
            { IsBackground = true }.Start();

            State = TaskState.Working;
        }

        /// <summary>
        /// Execute task operate.
        /// </summary>
        protected abstract void Execute();
    }
}