using AocHelper.DataSources;

namespace AocHelper.Tests.DataFetching
{
    public class FileCacheTests
    {
        private const string TEST_FILE_PATH = @"..\..\..\TestData\FileCacheTestFiles";
        private const string TEST_FILE_DEST_PATH = @"..\..\..\..\.cache\Input\0";
        private static readonly string _data = "My test data" + Environment.NewLine + "in the file.";

        [OneTimeSetUp]
        public static void Startup()
        {
            Directory.CreateDirectory(TEST_FILE_DEST_PATH);
            foreach (string file in Directory.EnumerateFiles(TEST_FILE_PATH))
            {
                string newFileName = Path.GetFileName(file);
                File.Copy(file, Path.Join(TEST_FILE_DEST_PATH, newFileName));
            }
        }

        [OneTimeTearDown]
        public static void Cleanup()
        {
            Directory.Delete(TEST_FILE_DEST_PATH, recursive: true);
        }

        [Test]
        public void TestCantFindFile()
        {
            // .../0/day0.txt
            var source = new FileCacheSource(0, 0);
            Assert.That(source.GetInput(out string reason), Is.False);
            Assert.That(reason.ToLower().Contains("file does not exist"), Is.True);
        }

        [Test]
        public void TestReadFile()
        {
            // .../0/day1.txt
            var source = new FileCacheSource(1, 0);
            Assert.That(source.GetInput(out string data), Is.True);
            Assert.That(_data, Is.EqualTo(data));
        }
    }
}