namespace AocHelper.DataStructures;

public static class DsUtils
{
    public static bool CollectionsEqual<T>(ICollection<T> first, ICollection<T> second)
    {
        if (first.Count != second.Count)
            return false;

        return first.Zip(second).All(pair => pair.First?.Equals(pair.Second) ?? false);
    }
}