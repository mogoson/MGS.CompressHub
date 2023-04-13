/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ITimeoutCacher.cs
 *  Description  :  Interface of timeout cacher.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Cachers
{
    /// <summary>
    /// Interface of timeout cacher.
    /// </summary>
    /// <typeparam name="T">Type of cache data.</typeparam>
    public interface ITimeoutCacher<T> : ICacher<T>
    {
        /// <summary>
        /// Timeout(ms).
        /// </summary>
        int Timeout { set; get; }
    }
}