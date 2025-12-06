using AocHelper;

namespace AocHelper.Tests
{
    public class InputSaveTests
    {
        private const string BASE_PATH = @"..\..\..\..\.cache\Input\0";
        private static readonly string _filePath = Path.Combine(BASE_PATH, "day0.txt");
        private InputSave _save = new(0, 0);

        [TearDown]
        public void Cleanup()
        {
            try
            {
                File.Delete(_filePath);
                Directory.Delete(BASE_PATH);
            }
            catch { }
        }

        [Test]
        public void TestSaveDataFileLocation()
        {
            _save.Save("Test string");
            Assert.That(Directory.Exists(BASE_PATH));
            Assert.That(File.Exists(_filePath));
        }

        [Test]
        public void TestSaveDataContent()
        {
            string testString = "Test string";
            _save.Save(testString);

            string actual = File.ReadAllText(_filePath);
            Assert.That(actual, Is.EqualTo(testString));
        }

        [Test]
        public void TestSaveDataOverride()
        {
            const string TEST_STRING1 = "Test string";
            const string TEST_STRING2 = "Test string 2";
            _save.Save(TEST_STRING1);

            _save.Save(TEST_STRING2);
            string actual = File.ReadAllText(_filePath);
            Assert.That(actual, Is.EqualTo(TEST_STRING1));

            _save.Save(TEST_STRING2, force:true);
            actual = File.ReadAllText(_filePath);
            Assert.That(actual, Is.EqualTo(TEST_STRING2));
        }
    }
}