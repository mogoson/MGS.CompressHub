/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncCompositeWork.cs
 *  Description  :  Async composite work abstract implement.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.Work
{
    /// <summary>
    /// Async composite work abstract implement.
    /// </summary>
    public class AsyncCompositeWork<T> : AsyncWork<T>
    {
        /// <summary>
        /// Speed(byte/s) of work..
        /// </summary>
        public override double Speed
        {
            get
            {
                if (work == null)
                {
                    return 0;
                }
                return work.Speed;
            }
        }

        /// <summary>
        /// Progress(0~1) of work.
        /// </summary>
        public override float Progress
        {
            get
            {
                if (work == null)
                {
                    return progress;
                }
                return progress + work.Progress * weights[step];
            }
        }

        /// <summary>
        /// Step works.
        /// </summary>
        protected IEnumerable<IAsyncWork> works;

        /// <summary>
        /// Weights for each work.
        /// </summary>
        protected float[] weights;

        /// <summary>
        /// Current execute work.
        /// </summary>
        protected IAsyncWork work;

        /// <summary>
        /// Progress of completed works.
        /// </summary>
        protected float progress;

        /// <summary>
        /// Current execute step.
        /// </summary>
        protected int step;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="works">Step works.</param>
        /// <param name="weights">Weights for each work.</param>
        public AsyncCompositeWork(ICollection<IAsyncWork> works, float[] weights = null)
        {
            this.works = works;
            if (weights == null)
            {
                weights = new float[works.Count];
                var weight = 1.0f / weights.Length;
                for (int i = 0; i < weights.Length; i++)
                {
                    weights[i] = weight;
                }
            }
            this.weights = weights;
        }

        /// <summary>
        /// On execute work operation.
        /// </summary>
        protected override void OnExecute()
        {
            step = -1;
            foreach (var work in works)
            {
                step++;
                this.work = work;
                work.Execute();

                if (work.Error != null)
                {
                    Error = work.Error;
                    return;
                }
                progress += work.Progress * weights[step];
            }
            Result = (T)work.Result;
        }

        /// <summary>
        /// On work finally.
        /// (Should override this method to set flag or clear ref object Only!)
        /// </summary>
        protected override void OnFinally()
        {
            base.OnFinally();

            works = null;
            work = null;
            weights = null;
        }
    }
}