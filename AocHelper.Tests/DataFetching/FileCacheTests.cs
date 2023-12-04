using AocHelper.DataSources;

namespace AocHelper.Tests.DataFetching
{
    [TestClass]
    public class FileCacheTests
    {
        private const string TEST_FILE_PATH = @"..\..\..\TestData\FileCacheTestFiles";
        private const string TEST_FILE_DEST_PATH = @"..\..\..\..\.cache\Input\0";
        private static readonly string _data = "My test data" + Environment.NewLine + "in the file.";

        [ClassInitialize]
        public static void Startup(TestContext _)
        {
            Directory.CreateDirectory(TEST_FILE_DEST_PATH);
            foreach (string file in Directory.EnumerateFiles(TEST_FILE_PATH))
            {
                string newFileName = Path.GetFileName(file);
                File.Copy(file, Path.Join(TEST_FILE_DEST_PATH, newFileName));
            }
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Directory.Delete(TEST_FILE_DEST_PATH, recursive: true);
        }

        [TestMethod]
        public void TestCantFindFile()
        {
            // .../0/day0.txt
            var source = new FileCacheSource(0, 0);
            Assert.IsFalse(source.GetInput(out string reason));
            Assert.IsTrue(reason.ToLower().Contains("file does not exist"));
        }

        [TestMethod]
        public void TestReadFile()
        {
            // .../0/day1.txt
            var source = new FileCacheSource(1, 0);
            Assert.IsTrue(source.GetInput(out string data));
            Assert.AreEqual(_data, data);
        }
    }
}