using System.Collections;
using System.Globalization;

namespace AocHelper.DataStructures
{
    public class Range : IEnumerable<long>
    {
        public long Start { get; }
        public long Length { get; }

        /// <summary>
        /// The value at the end of the range
        /// </summary>
        public long End => Start + Length - 1; 

        public Range(long start, long length)
        {
            Start = start;
            Length = length;
        }

        /// <summary>
        /// Create a range object from a start-end bound
        /// </summary>
        public static Range FromStartEnd(long start, long end)
        {
            return new Range(start, end - start + 1);
        }

        #region Overrides

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Range other)
                return false;

            return Start == other.Start && Length == other.Length;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return (Start, Length).GetHashCode();
        }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Iterate over all numbers in this range
        /// </summary>
        public IEnumerator<long> GetEnumerator()
        {
            for (long i = Start; i <= End; ++i)
                yield return i;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Whether the given range contains the number provided
        /// </summary>
        public bool Contains(long number)
        {
            return Start <= number && number <= End;
        }

        /// <summary>
        /// Does this range overlap at all with another range.
        /// If any elements are in both ranges then this is true.
        /// </summary>
        /// <param name="other">The other range to compare to.</param>
        public bool OverlapsWith(Range other)
        {
            return Contains(other.Start) || other.Contains(Start);
        }

        /// <summary>
        /// Creates a new range that encompasses the values of both ranges.
        /// There must be overlap between the 2 ranges.
        /// </summary>
        /// <param name="other">The other range to merge with</param>
        /// <returns>A new range</returns>
        /// <exception cref="ArgumentException">Thrown if the 2 ranges have no overlap</exception>
        public Range MergedWith(Range other)
        {
            if (!OverlapsWith(other))
                throw new ArgumentException("Ranges must have overlap to merge");

            long start = Start < other.Start ? Start : other.Start;
            long end = End > other.End ? End : other.End;

            return new Range(start, end - start + 1);
        }
    }
}
