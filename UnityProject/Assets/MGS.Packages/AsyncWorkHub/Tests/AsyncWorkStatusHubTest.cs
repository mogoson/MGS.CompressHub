using MGS.Work;
using NUnit.Framework;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AsyncWorkStatusHubTest
    {
        IAsyncWorkStatusHub hub;
        IAsyncWorkHandler<string> handler;
        Thread thread;

        [SetUp]
        public void SetUp()
        {
            var resultCacher = WorkHubFactory.CreateCacher<object>(100, 3000);
            var workCacher = WorkHubFactory.CreateCacher<IAsyncWork>(100);
            var resolver = WorkHubFactory.CreateResolver(3, null);
            hub = new AsyncWorkStatusHub(resultCacher, workCacher, 3, resolver);

            thread = new Thread(() =>
            {
                while (true)
                {
                    hub.TickStatus();
                    Thread.Sleep(100);
                }
            })
            { IsBackground = true };
            thread.Start();
        }

        [TearDown]
        public void TearDown()
        {
            thread.Abort();
            thread = null;

            handler.Dispose();
            handler = null;

            hub.Dispose();
            hub = null;
        }

        [UnityTest]
        public IEnumerator GetCallback()
        {
            var progress = 0f;
            string result = null;
            Exception error = null;

            handler = hub.EnqueueWork(new TestWork());
            handler.OnProgressChanged += p =>
            {
                progress = p;
                Debug.Log($"Progress {p}");
            };
            handler.OnSpeedChanged += speed =>
            {
                Debug.Log($"Speed {speed} byte/s");
            };
            handler.OnCompleted += (r, e) =>
            {
                result = r;
                error = e;

                if (e == null)
                {
                    Debug.Log($"Result {result}");
                }
                else
                {
                    Debug.Log($"Error {error.Message}/{error.StackTrace}");
                }
            };

            yield return handler.WaitDone();
            yield return new WaitForSeconds(1.0f);

            Assert.IsNull(error);
            Debug.Log($"work.progress {progress}");
            Assert.IsTrue(progress > 0);

            Debug.Log($"work.Result {result}");
            Assert.IsNotNull(result);
        }
    }
}