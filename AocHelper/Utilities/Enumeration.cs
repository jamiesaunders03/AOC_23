using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.Utilities
{
    public static class Enumeration
    {
        /// <summary>
        /// Enumerates over each element of a 2d array, yielding the indices and value of each pos
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
    }
}
