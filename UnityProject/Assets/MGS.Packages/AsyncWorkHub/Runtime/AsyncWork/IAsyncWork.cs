/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAsyncWork.cs
 *  Description  :  Interface of async work.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Work
{
    /// <summary>
    /// Interface of async work.
    /// </summary>
    public interface IAsyncWork : IDisposable
    {
        /// <summary>
        /// Key of work.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Timeout(ms) of work.
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// Work is done?
        /// </summary>
        bool IsDone { get; }

        /// <summary>
        /// Data size(byte) of work..
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Speed(byte/s) of work..
        /// </summary>
        double Speed { get; }

        /// <summary>
        /// Progress(0~1) of work.
        /// </summary>
        float Progress { get; }

        /// <summary>
        /// Result of work.
        /// </summary>
        object Result { get; }

        /// <summary>
        /// Error of work.
        /// </summary>
        Exception Error { get; }

        /// <summary>
        /// Execute work operation.
        /// </summary>
        void Execute();

        /// <summary>
        /// Execute work async operation.
        /// </summary>  
        void ExecuteAsync();

        /// <summary>
        /// Abort work async operation.
        /// </summary>
        void AbortAsync();
    }

    /// <summary>
    /// Interface of async work.
    /// </summary>
    /// <typeparam name="T">Type of work result.</typeparam>
    public interface IAsyncWork<T> : IAsyncWork
    {
        /// <summary>
        /// Result of work.
        /// </summary>
        new T Result { get; }
    }
}