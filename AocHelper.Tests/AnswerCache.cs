
using AocHelper.AnswerCache;

namespace AocHelper.Tests
{
    [TestClass]
    public class AnswerCache
    {
        private const int DAY = 0;
        private const int YEAR = 0;
        
        [TestCleanup]
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
        
        [TestMethod]
        public void TestAddClose()
        {
            Assert.IsTrue(StoreAnswer.SaveAnswer("123", new Incorrect(), DAY, YEAR));
            Assert.IsFalse(StoreAnswer.SaveAnswer("123", new Incorrect(), DAY, YEAR));
            Assert.IsTrue(StoreAnswer.SaveAnswer("124", new Incorrect(), DAY, YEAR));
        }
    }
}