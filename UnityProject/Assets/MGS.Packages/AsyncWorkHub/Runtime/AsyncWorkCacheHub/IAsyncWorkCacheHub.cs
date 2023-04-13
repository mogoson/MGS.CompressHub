/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAsyncWorkCacheHub.cs
 *  Description  :  Interface of hub to manage works and cache data.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Cachers;

namespace MGS.Work
{
    /// <summary>
    /// Interface of hub to manage works and cache data.
    /// </summary>
    public interface IAsyncWorkCacheHub : IAsyncWorkHub
    {
        /// <summary>
        /// Cacher for result.
        /// </summary>
        ICacher<object> ResultCacher { set; get; }

        /// <summary>
        /// Cacher for works.
        /// </summary>
        ICacher<IAsyncWork> WorksCacher { set; get; }
    }
}