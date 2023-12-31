using AocHelper.DataStructures;

namespace AocHelper.Tests
{
    [TestClass]
    public class BoolArrayTests
    {
        [TestMethod]
        public void TestLength()
        {
            BoolArray arr = new(20);
            Assert.ThrowsException<IndexOutOfRangeException>(() => arr[-1]);
            _ = arr[0];
            _ = arr[19];
            Assert.ThrowsException<IndexOutOfRangeException>(() => arr[20]);
        }

        [TestMethod]
        public void TestSet()
        {
            BoolArray arr = new(20);
            arr[1] = true;
            Assert.IsTrue(arr[1]);
            Assert.IsFalse(arr[0]);

            arr[1] = false;
            Assert.IsFalse(arr[1]);

            arr[8] = true;
            arr[15] = true;
            Assert.IsTrue(arr[8]);
            Assert.IsTrue(arr[15]);
        }
    }
}