using MGS.Cachers;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TimeoutCacherTests
    {
        ITimeoutCacher<string> cacher;

        [SetUp]
        public void SetUp()
        {
            cacher = new TimeoutCacher<string>(3, 1000);
        }

        [TearDown]
        public void TearDown()
        {
            cacher.Dispose();
            cacher = null;
        }

        [Test]
        public void TestSet()
        {
            cacher.Set("0", "0");//will discard.
            cacher.Set("1", "1");
            cacher.Set("2", "2");
            cacher.Set("3", "3");
            Assert.AreEqual(3, cacher.Count);
        }

        [UnityTest]
        public IEnumerator TestGet()
        {
            TestSet();

            var value0 = cacher.Get("0");//Already discard.
            Assert.AreEqual(null, value0);

            var value3 = cacher.Get("3");
            Assert.AreEqual("3", value3);

            yield return new WaitForSeconds(1.5f);

            var value1 = cacher.Get("1");//Already discard, timeout.
            Assert.AreEqual(null, value1);
        }
    }
}