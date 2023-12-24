using AocHelper.Utilities;

namespace AocHelper.Tests
{
    [TestClass]
    public class HashContainerTests
    {
        [TestMethod]
        public void TestHashContainerHashMethod()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = arr1.ToArray();

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);

            Assert.AreEqual(hc1.GetHashCode(), hc2.GetHashCode());
        }

        [TestMethod]
        public void TestHashContainerEq()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);

            Assert.AreEqual(hc1, hc2);
        }

        [TestMethod]
        public void TestHashingHashContainer()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);
            HashSet<HashContainer<int>> container = new() { hc1, };

            Assert.IsTrue(container.Contains(hc1));
            Assert.IsTrue(container.Contains(hc2));
        }

        [TestMethod]
        public void TestHashContainerAsKey()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);
            Dictionary<HashContainer<int>, int> container = new() { [hc1] = 1 };

            Assert.IsTrue(container.TryGetValue(hc2, out int x));
            Assert.AreEqual(x, 1);
        }
    }
}