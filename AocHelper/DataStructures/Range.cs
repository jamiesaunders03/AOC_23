namespace AocHelper.DataStructures
{
    public class Range
    {
        public long Start { get; }
        public long Length { get; }

        public Range(long start, long length)
        {
            Start = start;
            Length = length;
        }

        /// <summary>
        /// Whether the given range contains the number provided
        /// </summary>
        public bool Contains(long number)
        {
            return Start <= number && number <= Start + Length;
        }
    }
}
