
namespace AocHelper.Utilities
{
    public static class Enumeration
    {
        /// <summary>
        /// Enumerates over each element of a 2d array, yielding the indices and value of each pos
        /// The location tuple returns elements as a (y, x) tuple
        /// </summary>
        /// <param name="array">The array to enumerate</param>
        /// <returns></returns>
        public static IEnumerable<((int, int), T)> EnumerateArray<T>(T[,] array)
        {
            int h = array.GetLength(0);
            int w = array.GetLength(1);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    yield return ((i, j), array[i, j]);
                }
            }
        }

        /// <summary>
        /// Enumerates over each element of a 2d array, yielding the indices and value of each pos
        /// Special case for string array
        /// The location tuple returns elements as a (y, x) tuple
        /// </summary>
        /// <param name="array">The array to enumerate</param>
        /// <returns></returns>
        public static IEnumerable<((int, int), char)> EnumerateArray(string[] array)
        {
            int h = array.Length;
            int w = array[0].Length;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    yield return ((i, j), array[i][j]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opts">The options to generate the permutations from</param>
        /// <param name="len">The length of the permutation to generate</param>
        /// <returns></returns>
        public static IEnumerable<List<T>> PermutationsFrom<T>(ICollection<T> opts, int len)
        {
            int opsLen = opts.Count;
            int iterations = (int)System.Math.Pow(opsLen, len);

            for (int i = 0; i < iterations; ++i)
            {
                var perm = new List<T>();
                for (int op = 0; op < len; ++op)
                {
                    int index = (i / (int)System.Math.Pow(opsLen, op)) % opsLen;
                    perm.Add(opts.ElementAt(index));   
                }

                yield return perm;
            }
        }
    }
}
