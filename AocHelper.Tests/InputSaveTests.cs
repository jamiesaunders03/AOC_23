using AocHelper;

namespace AocHelper.Tests
{
    [TestClass]
    public class InputSaveTests
    {
        private const string BASE_PATH = @"..\..\..\..\.cache\Input\0";
        private static readonly string _filePath = Path.Combine(BASE_PATH, "day0.txt");
        private InputSave _save = new(0, 0);

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                File.Delete(_filePath);
                Directory.Delete(BASE_PATH);
            }
            catch { }
        }

        [TestMethod]
        public void TestSaveDataFileLocation()
        {
            _save.Save("Test string");
            Assert.IsTrue(Directory.Exists(BASE_PATH));
            Assert.IsTrue(File.Exists(_filePath));
        }

        [TestMethod]
        public void TestSaveDataContent()
        {
            string testString = "Test string";
            _save.Save(testString);

            string actual = File.ReadAllText(_filePath);
            Assert.AreEqual(testString, actual);
        }

        [TestMethod]
        public void TestSaveDataOverride()
        {
            const string testString1 = "Test string";
            const string testString2 = "Test string 2";
            _save.Save(testString1);

            _save.Save(testString2);
            string actual = File.ReadAllText(_filePath);
            Assert.AreEqual(testString1, actual);

            _save.Save(testString2, force:true);
            actual = File.ReadAllText(_filePath);
            Assert.AreEqual(testString2, actual);
        }
    }
}