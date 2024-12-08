using System.Collections;

namespace AocHelper.Tests;

public static class TestUtilities
{
    /// <summary>
    /// Provides the ability to consistently generate a 'hash' value based on the contents of a colleciton
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static int HashCollection(ICollection collection)
    {
        int hc = 13;

        unchecked
        {
            hc = collection
                .Cast<object>()
                .Aggregate(hc, (current, o) => (current + 17) * o.GetHashCode());
        }

        return hc;
    }
}