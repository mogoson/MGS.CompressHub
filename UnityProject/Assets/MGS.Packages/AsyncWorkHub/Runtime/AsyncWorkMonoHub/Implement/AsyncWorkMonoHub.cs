/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncWorkMonoHub.cs
 *  Description  :  Hub to manage work and cache data,
 *                  and unity main thread notify status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

#if UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
using MGS.Cachers;
using System.Collections;
using UnityEngine;

namespace MGS.Work
{
    /// <summary>
    /// Hub to manage work and cache data,
    /// and main thread notify status.
    /// </summary>
    public class AsyncWorkMonoHub : AsyncWorkStatusHub, IAsyncWorkMonoHub
    {
        /// <summary>
        /// WorkHubBehaviour to handle MonoBehaviour.
        /// </summary>
        protected class WorkHubBehaviour : MonoBehaviour { }

        /// <summary>
        /// MonoBehaviour for hub to StartCoroutine.
        /// </summary>
        protected WorkHubBehaviour behaviour;

        /// <summary>
        /// Yield Instruction for tick update.
        /// </summary>
        protected YieldInstruction instruction;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="resultCacher">Cacher for result.</param>
        /// <param name="workCacher">Cacher for work.</param>
        /// <param name="concurrency">Max count of concurrency works.</param>
        /// <param name="resolver">Resolver to check retrieable.</param>
        public AsyncWorkMonoHub(ICacher<object> resultCacher = null,
            ICacher<IAsyncWork> workCacher = null, int concurrency = 3, IWorkResolver resolver = null)
            : base(resultCacher, workCacher, concurrency, resolver)
        {
            instruction = new WaitForSeconds(TICK_CYCLE * 0.001f);
            behaviour = new GameObject(typeof(WorkHubBehaviour).Name).AddComponent<WorkHubBehaviour>();
            behaviour.StartCoroutine(TickUpdate());
            Object.DontDestroyOnLoad(behaviour.gameObject);
        }

        /// <summary>
        /// Dispose all resource.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            instruction = null;
            if (behaviour != null)
            {
                Object.Destroy(behaviour.gameObject);
                behaviour = null;
            }
        }

        /// <summary>
        /// MonoBehaviour tick to update.
        /// </summary>
        /// <returns></returns>
        private IEnumerator TickUpdate()
        {
            while (!isDisposed)
            {
                TickStatus();
                yield return instruction;
            }
        }
    }
}
#endif