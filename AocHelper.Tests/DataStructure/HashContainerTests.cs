using AocHelper.Utilities;

namespace AocHelper.Tests.DataStructure
{
    public class HashContainerTests
    {
        [Test]
        public void TestHashContainerHashMethod()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = arr1.ToArray();

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);

            Assert.That(hc1.GetHashCode(), Is.EqualTo(hc2.GetHashCode()));
        }

        [Test]
        public void TestHashContainerEq()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);

            Assert.That(hc1, Is.EqualTo(hc2));
        }

        [Test]
        public void TestHashingHashContainer()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);
            HashSet<HashContainer<int>> container = [ hc1 ];

            Assert.That(container.Contains(hc1), Is.True);
            Assert.That(container.Contains(hc2), Is.True);
        }

        [Test]
        public void TestHashContainerAsKey()
        {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            HashContainer<int> hc1 = new(arr1);
            HashContainer<int> hc2 = new(arr2);
            Dictionary<HashContainer<int>, int> container = new() { [hc1] = 1 };

            Assert.That(container.TryGetValue(hc2, out int x), Is.True);
            Assert.That(x, Is.EqualTo(1));
        }
    }
}