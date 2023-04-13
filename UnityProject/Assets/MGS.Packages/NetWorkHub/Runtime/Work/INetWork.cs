/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  INetWork.cs
 *  Description  :  Interface of work to connect remote.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Work.Net
{
    /// <summary>
    /// Interface of work to connect remote.
    /// </summary>
    public interface INetWork<T> : IAsyncWork<T>
    {
        /// <summary>
        /// Remote url string.
        /// </summary>
        string URL { get; }
    }
}