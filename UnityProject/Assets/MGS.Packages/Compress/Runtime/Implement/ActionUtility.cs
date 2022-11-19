/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ActionUtility.cs
 *  Description  :  Utility for action.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  11/28/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Compress
{
    /// <summary>
    /// Utility for action.
    /// </summary>
    internal sealed class ActionUtility
    {
        /// <summary>
        /// Invoke action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="arg"></param>
        public static void Invoke<T>(Action<T> action, T arg)
        {
            if (action != null)
            {
                action.Invoke(arg);
            }
        }

        /// <summary>
        /// Invoke action.
        /// </summary>
        /// <typeparam name="T0"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="action"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public static void Invoke<T0, T1, T2>(Action<T0, T1, T2> action, T0 arg0, T1 arg1, T2 arg2)
        {
            if (action != null)
            {
                action.Invoke(arg0, arg1, arg2);
            }
        }
    }
}