/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAsyncWorkMonoHub.cs
 *  Description  :  Interface for hub to manage work and cache data,
 *                  and unity main thread notify status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

#if UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
namespace MGS.Work
{
    /// <summary>
    /// Interface for hub to manage work and cache data,
    /// and unity main thread notify status.
    /// </summary>
    public interface IAsyncWorkMonoHub : IAsyncWorkStatusHub { }
}
#endif