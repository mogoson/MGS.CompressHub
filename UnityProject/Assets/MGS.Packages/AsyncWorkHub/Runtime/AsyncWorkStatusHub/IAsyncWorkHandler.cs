/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAsyncWorkHandler.cs
 *  Description  :  Interface of async handler.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;

namespace MGS.Work
{
    /// <summary>
    /// Interface of async handler.
    /// </summary>
    public interface IAsyncWorkHandler : IDisposable
    {
        /// <summary>
        /// Handle work;
        /// </summary>
        IAsyncWork Work { get; }

        /// <summary>
        /// Notify status of work.
        /// </summary>
        void NotifyStatus();

        /// <summary>
        /// Wait the work done.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitDone();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncWorkHandler<T> : IAsyncWorkHandler
    {
        /// <summary>
        /// Handle work;
        /// </summary>
        new IAsyncWork<T> Work { get; }

        /// <summary>
        /// On speed changed event.
        /// </summary>
        event Action<double> OnSpeedChanged;

        /// <summary>
        /// On progress changed event.
        /// </summary>
        event Action<float> OnProgressChanged;

        /// <summary>
        /// On completed event.
        /// </summary>
        event Action<T, Exception> OnCompleted;
    }
}