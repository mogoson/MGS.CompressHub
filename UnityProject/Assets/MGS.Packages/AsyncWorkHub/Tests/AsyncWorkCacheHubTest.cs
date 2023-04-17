using MGS.Work;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AsyncWorkCacheHubTest
    {
        IAsyncWorkCacheHub hub;
        IAsyncWork<string> work;

        [SetUp]
        public void SetUp()
        {
            hub = WorkHubFactory.CreateCacheHub(3);
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
            var key = "Key to test cache.";

            var hashCode = hub.EnqueueWork(new TestWork(key)).GetHashCode();
            work = hub.EnqueueWork(new TestWork(key));//Work object cache.

            Assert.AreEqual(work.GetHashCode(), hashCode);

            while (!work.IsDone)
            {
                yield return null;
                Debug.Log($"Progress {work.Progress}");
            }

            Assert.IsNull(work.Error);
            Debug.Log($"work.Result {work.Result}");
            Assert.IsNotNull(work.Result);

            yield return new WaitForSeconds(0.5f);
            work = hub.EnqueueWork(new TestWork(key));//Result object cache.
            Assert.AreEqual(work.GetType(), typeof(CacheWork<string>));

            Assert.IsNull(work.Error);
            Debug.Log($"work.Result {work.Result}");
            Assert.IsNotNull(work.Result);
        }
    }
}