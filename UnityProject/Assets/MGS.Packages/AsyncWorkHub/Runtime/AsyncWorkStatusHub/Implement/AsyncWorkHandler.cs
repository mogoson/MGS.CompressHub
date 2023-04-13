/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWorkHandler.cs
 *  Description  :  Handler to manage work status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Work
{
    /// <summary>
    /// Handler to manage work status.
    /// </summary>
    public class AsyncWorkHandler<T> : IAsyncWorkHandler<T>
    {
        /// <summary>
        /// Work of handler.
        /// </summary>
        IAsyncWork IAsyncWorkHandler.Work { get { return Work; } }

        /// <summary>
        /// Work of handler.
        /// </summary>
        public IAsyncWork<T> Work { protected set; get; }

        /// <summary>
        /// On speed changed event.
        /// </summary>
        public event Action<double> OnSpeedChanged;

        /// <summary>
        /// On progress changed event.
        /// </summary>
        public event Action<float> OnProgressChanged;

        /// <summary>
        /// On completed event.
        /// </summary>
        public event Action<T, Exception> OnCompleted;

        /// <summary>
        /// Last speed value.
        /// </summary>
        protected double speed;

        /// <summary>
        /// Last progress value.
        /// </summary>
        protected float progress;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="work"></param>
        public AsyncWorkHandler(IAsyncWork<T> work)
        {
            Work = work;
        }

        /// <summary>
        /// Notify status of work.
        /// </summary>
        public virtual void NotifyStatus()
        {
            if (speed != Work.Speed)
            {
                speed = Work.Speed;
                InvokeOnSpeedChanged(speed);
            }

            if (progress != Work.Progress)
            {
                progress = Work.Progress;
                InvokeOnProgressChanged(progress);
            }

            if (Work.IsDone)
            {
                //Is not abort.
                if (Work.Result != null || Work.Error != null)
                {
                    InvokeOnCompleted(Work.Result, Work.Error);
                }
            }
        }

        /// <summary>
        /// Dispose all resources.
        /// </summary>
        public virtual void Dispose()
        {
            Work.Dispose();
            Work = null;
            ClearEvents();
        }

        /// <summary>
        /// Clear callback events.
        /// </summary>
        protected void ClearEvents()
        {
            OnSpeedChanged = null;
            OnProgressChanged = null;
            OnCompleted = null;
        }

        /// <summary>
        /// Invoke OnSpeedChanged event.
        /// </summary>
        /// <param name="speed"></param>
        protected void InvokeOnSpeedChanged(double speed)
        {
            OnSpeedChanged?.Invoke(speed);
        }

        /// <summary>
        /// Invoke OnProgressChanged event.
        /// </summary>
        /// <param name="progress"></param>
        protected void InvokeOnProgressChanged(float progress)
        {
            OnProgressChanged?.Invoke(progress);
        }

        /// <summary>
        /// Invoke OnCompleted event.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="error"></param>
        protected void InvokeOnCompleted(T result, Exception error)
        {
            OnCompleted?.Invoke(Work.Result, Work.Error);
        }
    }
}