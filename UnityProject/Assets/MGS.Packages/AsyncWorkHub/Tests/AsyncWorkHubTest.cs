using MGS.Work;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AsyncWorkHubTest
    {
        IAsyncWorkHub hub;
        IAsyncWork<string> work;

        [SetUp]
        public void SetUp()
        {
            hub = WorkHubFactory.CreateHub(3);
        }

        [TearDown]
        public void TearDown()
        {
            work.Dispose();
            work = null;

            hub.Dispose();
            hub = null;
        }

        [UnityTest]
        public IEnumerator TestEnqueueWork()
        {
            work = hub.EnqueueWork(new TestWork());
            while (!work.IsDone)
            {
                yield return null;
                Debug.Log($"Progress {work.Progress}");
            }

            Assert.IsNull(work.Error);
            Debug.Log($"work.Result {work.Result}");
            Assert.IsNotNull(work.Result);
        }

        [UnityTest]
        public IEnumerator TestConcurrency()
        {
            work = hub.EnqueueWork(new TestWork());
            work = hub.EnqueueWork(new TestWork());
            work = hub.EnqueueWork(new TestWork());
            work = hub.EnqueueWork(new TestWork());

            yield return new WaitForSeconds(3.0f);

            Debug.Log($"hub.Waitings {hub.Waitings}");
            Debug.Log($"hub.Workings {hub.Workings}");

            Assert.AreEqual(hub.Waitings, 1);
            Assert.AreEqual(hub.Workings, 3);
        }

        [UnityTest]
        public IEnumerator TestClear()
        {
            work = hub.EnqueueWork(new TestWork());
            work = hub.EnqueueWork(new TestWork());
            work = hub.EnqueueWork(new TestWork());

            yield return new WaitForSeconds(3.0f);
            hub.Clear(true, true);

            Debug.Log($"hub.Waitings {hub.Waitings}");
            Debug.Log($"hub.Workings {hub.Workings}");

            Assert.AreEqual(hub.Waitings, 0);
            Assert.AreEqual(hub.Workings, 0);
        }
    }
}