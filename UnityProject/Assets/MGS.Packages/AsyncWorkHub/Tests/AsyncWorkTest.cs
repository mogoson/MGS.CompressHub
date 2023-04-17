using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AsyncWorkTest
    {
        [Test]
        public void TestExecute()
        {
            var work = new TestWork();
            work.Execute();

            Assert.IsNull(work.Error);
            Debug.Log($"work.Result {work.Result}");
            Assert.IsNotNull(work.Result);
        }

        [Test]
        public void TestExecuteError()
        {
            var work = new TestErrorWork();
            work.Execute();

            Assert.IsNull(work.Result);
            Assert.IsNotNull(work.Error);
            Debug.Log($"work.Error {work.Error.Message}");
        }

        [UnityTest]
        public IEnumerator TestExecuteAsync()
        {
            var work = new TestWork();
            work.ExecuteAsync();

            while (!work.IsDone)
            {
                yield return null;
                Debug.Log($"Speed {work.Speed} byte/s");
                Debug.Log($"Progress {work.Progress}");
            }

            Assert.IsNull(work.Error);
            Debug.Log($"work.Result {work.Result}");
            Assert.IsNotNull(work.Result);
        }
    }
}