using AocHelper.DataStructures;

namespace AocHelper.Tests.DataStructure
{
    public class BoolArrayTests
    {
        [Test]
        public void TestLength()
        {
            BoolArray arr = new(20);
            Assert.That(() => arr[-1], Throws.InstanceOf<IndexOutOfRangeException>());
            _ = arr[0];
            _ = arr[19];
            Assert.That(() => arr[20], Throws.InstanceOf<IndexOutOfRangeException>());
        }

        [Test]
        public void TestSet()
        {
            BoolArray arr = new(20);
            arr[1] = true;
            Assert.That(arr[1], Is.True);
            Assert.That(arr[0], Is.False);

            arr[1] = false;
            Assert.That(arr[1], Is.False);

            arr[8] = true;
            arr[15] = true;
            Assert.That(arr[8], Is.True);
            Assert.That(arr[15], Is.True);
        }
    }
}