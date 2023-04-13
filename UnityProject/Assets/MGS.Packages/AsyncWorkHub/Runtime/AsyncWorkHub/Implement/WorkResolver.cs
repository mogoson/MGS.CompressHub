/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  WorkResolver.cs
 *  Description  :  Resolver to check work retrieable.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.Work
{
    /// <summary>
    /// Resolver to check work retrieable.
    /// </summary>
    public class WorkResolver : IWorkResolver
    {
        /// <summary>
        /// Retry times.
        /// </summary>
        protected int times;

        /// <summary>
        /// Tolerable exception types can be retry.
        /// </summary>
        protected ICollection<Type> tolerables;

        /// <summary>
        /// Tolerance times.
        /// </summary>
        protected Dictionary<string, int> toleranceTimes;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="times">Retry times.</param>
        /// <param name="tolerables">Tolerable exception types can be retry.</param>
        public WorkResolver(int times, ICollection<Type> tolerables)
        {
            this.times = times;
            this.tolerables = tolerables;
            toleranceTimes = new Dictionary<string, int>();
        }

        /// <summary>
        /// Check check is retrieable?
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public bool Retrieable(IAsyncWork work)
        {
            if (tolerables == null || !tolerables.Contains(work.Error.GetType()))
            {
                return false;
            }

            var tts = 0;
            if (toleranceTimes.ContainsKey(work.Key))
            {
                tts = toleranceTimes[work.Key];
            }

            if (tts < times)
            {
                toleranceTimes[work.Key] = tts + 1;
                return true;
            }
            else
            {
                Clear(work);
                return false;
            }
        }

        /// <summary>
        /// Clear the history of work.
        /// </summary>
        /// <param name="work"></param>
        public void Clear(IAsyncWork work)
        {
            toleranceTimes.Remove(work.Key);
        }

        /// <summary>
        /// Clear the history of all works.
        /// </summary>
        public void Clear()
        {
            toleranceTimes.Clear();
        }

        /// <summary>
        /// Dispose resolver.
        /// </summary>
        public void Dispose()
        {
            tolerables = null;
            toleranceTimes = null;
        }
    }
}