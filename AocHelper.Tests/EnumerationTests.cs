using AocHelper.Utilities;

namespace AocHelper.Tests;

[TestClass]
public class EnumerationTests
{
    [TestMethod]
    public void TestPermutations()
    {
        int[] numbers = [ 1, 2 ];
        
        List<int>[] enumerations = Enumeration.PermutationsFrom(numbers, 3).ToArray();
        var expected = new List<int>[]
        {
            [1, 1, 1,],
            [2, 1, 1,],
            [1, 2, 1,],
            [2, 2, 1,],
            [1, 1, 2,],
            [2, 1, 2,],
            [1, 2, 2,],
            [2, 2, 2,],
        };
        
        Assert.AreEqual(expected.Length, enumerations.Length);
        for (int i = 0; i < expected.Length; ++i)
        {
            CollectionAssert.AreEquivalent(expected[i], enumerations.ElementAt(i));
        }
    }

    [TestMethod]
    public void TestCombinations()
    {
        var nums = new List<int> { 1, 2, 3, };
        IEnumerable<List<int>> oneLen = Enumeration.Combinations(nums, 1);
        
        CollectionAssert.AreEquivalent(
            nums.Select(i => TestUtilities.HashCollection(new List<int> { i, })).ToList(),
            oneLen.Select(TestUtilities.HashCollection).ToList()
            );
        
        IEnumerable<List<int>> twoLen = Enumeration.Combinations(nums, 2);
        List<List<int>> perms =
        [
            [1, 2],
            [1, 3],
            [2, 3],
        ];
        CollectionAssert.AreEquivalent(
            perms.Select(TestUtilities.HashCollection).ToList(),
            twoLen.Select(TestUtilities.HashCollection).ToList()
        );
        
        IEnumerable<List<int>> threeLen = Enumeration.Combinations(nums, 3);
        perms = [ [1, 2, 3] ];
        CollectionAssert.AreEquivalent(
            perms.Select(TestUtilities.HashCollection).ToList(),
            threeLen.Select(TestUtilities.HashCollection).ToList()
        );
    }

    [TestMethod]
    public void TestCombinationsInvalidValues()
    {
        var lst = new List<int> { 1, 2, 3, };
        Assert.ThrowsException<ArgumentException>(() => Enumeration.Combinations(lst, 0));
        Assert.ThrowsException<ArgumentException>(() => Enumeration.Combinations(lst, 4));
    }
}