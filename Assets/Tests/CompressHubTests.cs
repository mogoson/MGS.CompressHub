using MGS.Work;
using MGS.Work.Compress;
using NUnit.Framework;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CompressHubTests
    {
        ICompressHub hub;
        IAsyncWorkHandler<string> handler;

        [SetUp]
        public void SetUp()
        {
#if true
            var compressor = new IonicCompressor();
#else
            var compressor = new SharpCompressor();
#endif
            hub = CompressHubFactory.CreateHub(compressor);
        }

        [TearDown]
        public void TearDown()
        {
            handler.Abort();
            handler = null;

            hub.Abort();
            hub = null;
        }

        [UnityTest]
        public IEnumerator TestCompressAsync()
        {
            var zipDir = $"{Application.dataPath}/../TestDir/TestZipDir";
            if (!Directory.Exists(zipDir))
            {
                Directory.CreateDirectory(zipDir);
            }
            var testFile = $"{zipDir}/TestFile.txt";
            if (!File.Exists(testFile))
            {
                File.WriteAllText(testFile, "This is a test file.");
            }

            var zipFile = $"{Application.dataPath}/../TestDir/TestZip.zip";
            var rootDir = "TestRootDir";

            handler = hub.CompressAsync(new string[] { zipDir }, zipFile, Encoding.UTF8, rootDir, true);
            handler.OnProgressChanged += p =>
            {
                Debug.Log($"Progress {p}");
            };
            handler.OnCompleted += (r, e) =>
            {
                if (e == null)
                {
                    Debug.Log($"Result {r}");
                }
                else
                {
                    Debug.Log($"Error {e.Message}/{e.StackTrace}");
                }
            };
            yield return handler.WaitDone();

            Assert.IsNull(handler.Work.Error);
            Debug.Log($"work.Result {handler.Work.Result}");
            Assert.IsNotNull(handler.Work.Result);
        }

        [UnityTest]
        public IEnumerator TestDecompressAsync()
        {
            var filePath = $"{Application.dataPath}/../TestDir/TestZip.zip";
            var unzipDirPath = $"{Application.dataPath}/../TestDir/TestUnzipDir/";

            handler = hub.DecompressAsync(filePath, unzipDirPath, true);
            handler.OnProgressChanged += p =>
            {
                Debug.Log($"Progress {p}");
            };
            handler.OnCompleted += (r, e) =>
            {
                if (e == null)
                {
                    Debug.Log($"Result {r}");
                }
                else
                {
                    Debug.Log($"Error {e.Message}/{e.StackTrace}");
                }
            };
            yield return handler.WaitDone();

            Assert.IsNull(handler.Work.Error);
            Debug.Log($"work.Result {handler.Work.Result}");
            Assert.IsNotNull(handler.Work.Result);
        }
    }
}