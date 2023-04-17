using MGS.Work;
using System;
using System.Threading;
using UnityEngine;

namespace Tests
{
    public class TestWork : AsyncWork<string>
    {
        public TestWork(string key = null)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Key = key;
            }
        }

        protected override void OnExecute()
        {
            Thread.Sleep(1000);
            Progress += 0.25f;
            Debug.Log($"Work {Key} OnExecute Progress {Progress}");

            Thread.Sleep(1000);
            Progress += 0.25f;
            Debug.Log($"Work {Key} OnExecute Progress {Progress}");

            Thread.Sleep(1000);
            Progress += 0.25f;
            Debug.Log($"Work {Key} OnExecute Progress {Progress}");

            Thread.Sleep(1000);
            Progress += 0.25f;
            Debug.Log($"Work {Key} OnExecute Progress {Progress}");
            Result = "Result of TestWork";
        }
    }

    public class TestErrorWork : AsyncWork<string>
    {
        protected override void OnExecute()
        {
            Thread.Sleep(1000);
            Progress += 0.25f;
            Debug.Log($"Work {Key} OnExecute Progress {Progress}");

            throw new Exception("We throw an exception to test error.");
        }
    }
}