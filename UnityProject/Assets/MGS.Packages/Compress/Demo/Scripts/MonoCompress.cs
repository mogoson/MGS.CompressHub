/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCompress.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Compress.Demo
{
    public abstract class MonoCompress : MonoBehaviour
    {
        Queue<Action> actions = new Queue<Action>();

        void Update()
        {
            lock (actions)
            {
                while (actions.Count > 0)
                {
                    actions.Dequeue().Invoke();
                }
            }
        }

        protected void BeginInvoke(Action action)
        {
            lock (actions)
            {
                actions.Enqueue(action);
            }
        }
    }
}