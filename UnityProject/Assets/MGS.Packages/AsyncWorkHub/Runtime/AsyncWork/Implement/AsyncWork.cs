/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWork.cs
 *  Description  :  Async work abstract implement.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Threading;

namespace MGS.Work
{
    /// <summary>
    /// Async work abstract implement.
    /// </summary>
    public abstract class AsyncWork<T> : IAsyncWork<T>
    {
        /// <summary>
        /// Key of work.
        /// </summary>
        public virtual string Key { protected set; get; }

        /// <summary>
        /// Timeout(ms) of work.
        /// </summary>
        public virtual int Timeout { protected set; get; }

        /// <summary>
        /// Work is done?
        /// </summary>
        public virtual bool IsDone { protected set; get; }

        /// <summary>
        /// Data size(byte) of work..
        /// </summary>
        public virtual long Size { protected set; get; }

        /// <summary>
        /// Speed(byte/s) of work..
        /// </summary>
        public virtual double Speed { protected set; get; }

        /// <summary>
        /// Progress(0~1) of work.
        /// </summary>
        public virtual float Progress { protected set; get; }

        /// <summary>
        /// Result of work.
        /// </summary>
        object IAsyncWork.Result { get { return Result; } }

        /// <summary>
        /// Result of work.
        /// </summary>
        public virtual T Result { protected set; get; }

        /// <summary>
        /// Error of work.
        /// </summary>
        public virtual Exception Error { protected set; get; }

        /// <summary>
        /// Thread to do work.
        /// </summary>
        protected Thread thread;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AsyncWork()
        {
            Key = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Execute work operation.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Execute work async..
        /// </summary>
        public virtual void ExecuteAsync()
        {
            if (thread == null)
            {
                Reset();
                thread = new Thread(() =>
                {
                    try
                    {
                        Execute();
                    }
                    catch (Exception ex)
                    {
                        Error = ex;
                        AbortAsync();
                    }
                    finally
                    {
                        OnFinally();
                    }
                })
                { IsBackground = true };
                thread.Start();
            }
        }

        /// <summary>
        /// Abort work async operation.
        /// </summary>
        public virtual void AbortAsync()
        {
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    try { thread.Abort(); }
                    catch { }
                }
                thread = null;
            }
            IsDone = true;
        }

        /// <summary>
        /// Dispose work.
        /// </summary>
        public virtual void Dispose()
        {
            AbortAsync();
            Reset();
        }

        /// <summary>
        /// Reset work.
        /// </summary>
        protected virtual void Reset()
        {
            IsDone = false;
            Size = 0;
            Speed = 0;
            Progress = 0;
            Result = default;
            Error = null;
        }

        /// <summary>
        /// On work finally.
        /// (Should override this method to set flag or clear ref object Only!)
        /// </summary>
        protected virtual void OnFinally()
        {
            thread = null;
            IsDone = true;
        }
    }
}