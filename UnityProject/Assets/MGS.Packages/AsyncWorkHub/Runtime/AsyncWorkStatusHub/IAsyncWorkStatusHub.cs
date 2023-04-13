/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAsyncWorkStatusHub.cs
 *  Description  :  Interface of hub to manage work and cache data,
 *                  and let other thread notify status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Work
{
    /// <summary>
    /// Interface of hub to manage work and cache data,
    /// and let other thread notify status.
    /// </summary>
    public interface IAsyncWorkStatusHub : IAsyncWorkCacheHub
    {
        /// <summary>
        /// Enqueue work to hub.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        new IAsyncWorkHandler<T> EnqueueWork<T>(IAsyncWork<T> work);

        /// <summary>
        /// Tick update to notify status.
        /// </summary>
        void TickStatus();
    }
}