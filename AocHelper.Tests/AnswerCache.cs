using AocHelper.AnswerCache;

namespace AocHelper.Tests
{
    [TestClass]
    public class AnswerCache
    {
        [TestMethod]
        public void TestAddClose()
        {
            Assert.IsTrue(StoreAnswer.SaveAnswer("123", new Incorrect(), 0, 0));
            Assert.IsFalse(StoreAnswer.SaveAnswer("123", new Incorrect(), 0, 0));
            Assert.IsTrue(StoreAnswer.SaveAnswer("124", new Incorrect(), 0, 0));
        }
    }
}