using AocHelper.AnswerCache;

namespace AocHelper.Tests
{
    [TestClass]
    public class AnswerCache
    {
        [TestMethod]
        public void TestAddClose()
        {
            Assert.IsTrue(StoreAnswer.SaveAnswer("123", AnswerState.NOT_QUITE, 0, 0));
            Assert.IsFalse(StoreAnswer.SaveAnswer("123", AnswerState.NOT_QUITE, 0, 0));
            Assert.IsTrue(StoreAnswer.SaveAnswer("124", AnswerState.NOT_QUITE, 0, 0));
        }
    }
}