using AocHelper.Utilities;
using Math = System.Math;

namespace AocHelper.Tests;

public class EnumerationTests
{
    [Test]
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
        
        Assert.That(expected, Is.EquivalentTo(enumerations));
    }

    [Test]
    public void TestCombinations()
    {
        var nums = new List<int> { 1, 2, 3, };
        IEnumerable<List<int>> oneLen = Enumeration.Combinations(nums, 1);
        
        Assert.That(
            nums.Select(i => TestUtilities.HashCollection(new List<int> { i })).ToList(),
            Is.EquivalentTo(oneLen.Select(TestUtilities.HashCollection).ToList()));
        
        IEnumerable<List<int>> twoLen = Enumeration.Combinations(nums, 2);
        List<List<int>> perms =
        [
            [1, 2],
            [1, 3],
            [2, 3],
        ];
        Assert.That(
            perms.Select(TestUtilities.HashCollection).ToList(),
            Is.EquivalentTo(twoLen.Select(TestUtilities.HashCollection).ToList())
        );
        
        IEnumerable<List<int>> threeLen = Enumeration.Combinations(nums, 3);
        perms = [ [1, 2, 3] ];
        Assert.That(
            perms.Select(TestUtilities.HashCollection).ToList(),
            Is.EquivalentTo(threeLen.Select(TestUtilities.HashCollection).ToList())
        );
    }

    [Test]
    public void TestCombinationsInvalidValues()
    {
        var lst = new List<int> { 1, 2, 3, };
        Assert.That(() => Enumeration.Combinations(lst, 0), Throws.ArgumentException);
        Assert.That(() => Enumeration.Combinations(lst, 4), Throws.ArgumentException);
    }

    [Test]
    public void TestProduct()
    {
        List<long> items = [];
        Assert.That(items.Prod(), Is.Zero);

        items = [2, 4, 9, -2];
        Assert.That(items.Prod(), Is.EqualTo(-144));
        Assert.That(items.Select(Math.Abs).Prod(), Is.EqualTo(144));

        items = [0, 7, 1];
        Assert.That(items.Prod(), Is.Zero);
    }
}