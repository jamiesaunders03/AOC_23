
using Microsoft.VisualBasic.CompilerServices;

namespace AocHelper.DataStructures;

public class Pair<T1, T2>
{
    public T1 First { get; }
    public T2 Second { get; }

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }

    public static bool operator==(Pair<T1, T2> self, Pair<T1, T2> other)
    {
        return self.First.Equals(other.First) && self.Second.Equals(other.Second);
    }
    
    public static bool operator!=(Pair<T1, T2> self, Pair<T1, T2> other)
    {
        return !(self == other);
    }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Pair<T1, T2> { First: T1 f, Second: T2 s } && First.Equals(f) && Second.Equals(s);
    }

    /// <summary>Serves as the default hash function.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return 31 * First.GetHashCode() + Second.GetHashCode();
    }
}