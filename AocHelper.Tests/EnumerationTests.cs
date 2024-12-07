using System.Collections;
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
}