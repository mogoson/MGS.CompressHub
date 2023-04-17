using MGS.Cachers;
using NUnit.Framework;

namespace Tests
{
    public class CacherTests
    {
        ICacher<string> cacher;

        [SetUp]
        public void SetUp()
        {
            cacher = new Cacher<string>(3);
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

        [Test]
        public void TestGet()
        {
            TestSet();

            var value0 = cacher.Get("0");//Already discard.
            Assert.AreEqual(null, value0);

            var value3 = cacher.Get("3");
            Assert.AreEqual("3", value3);
        }
    }
}