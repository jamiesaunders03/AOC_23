
using AocHelper.AnswerCache;

namespace AocHelper.Tests
{
    public class AnswerCache
    {
        private const int DAY = 0;
        private const int YEAR = 0;
        
        [TearDown]
        public void Cleanup()
        {
            try
            {
                File.Delete(StoreAnswer.GetFilePath(YEAR, DAY));
            }
            catch (Exception e) when (e is DirectoryNotFoundException 
                                        or IOException 
                                        or UnauthorizedAccessException) 
            { }
        }
        
        [Test]
        public void TestAddClose()
        {
            Assert.That(StoreAnswer.SaveAnswer("123", new Incorrect(), DAY, YEAR), Is.True);
            Assert.That(StoreAnswer.SaveAnswer("123", new Incorrect(), DAY, YEAR), Is.False);
            Assert.That(StoreAnswer.SaveAnswer("124", new Incorrect(), DAY, YEAR), Is.True);
        }
    }
}