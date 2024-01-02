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
    }
}
