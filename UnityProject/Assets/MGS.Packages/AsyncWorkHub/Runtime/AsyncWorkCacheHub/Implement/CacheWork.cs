/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CacheWork.cs
 *  Description  :  Work for cache data.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Work
{
    /// <summary>
    /// Work for cache data.
    /// </summary>
    public class CacheWork<T> : AsyncWork<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="result">Result cache data.</param>
        public CacheWork(T result)
        {
            Result = result;
            IsDone = true;
        }

        /// <summary>
        /// On execute work operation.
        /// </summary>
        protected override void OnExecute() { }
    }
}