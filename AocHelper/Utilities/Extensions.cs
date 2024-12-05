using System.Collections;

namespace AocHelper.Utilities
{
    public static class Extensions
    {
        #region Array

        /// <summary>
        /// Populates an array with a value
        /// Note that for reference types this will mean the same reference is used for every element
        /// </summary>
        /// <param name="arr">The array to fill</param>
        /// <param name="value">The value to fill the array with</param>
        public static void Fill<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        /// <summary>
        /// Convert a 2d Array to an IEnumerable T
        /// </summary>
        /// <param name="target">The array to convert</param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this T[,] target)
        {
            foreach (T item in target)
                yield return item;
        }

        /// <summary>
        /// Converts an array of string to a 2D grid of chars.
        /// All strings must be the same length, otherwise a
        /// </summary>
        /// <param name="strs">Array of strings to create a grid from</param>
        /// <returns>2d array of chars, constructed from the original string array</returns>
        public static char[,] ToGrid(this string[] strs)
        {
            if (strs.Length == 0)
                return new char[,] { };

            int len = strs[0].Length;
            if (strs.Skip(1).Any(str => str.Length != len))
                throw new ArgumentException("Strings were not all the same length");

            char[,] grid = new char[strs.Length,len];
            for (int strIndex = 0; strIndex < strs.Length; ++strIndex)
                for (int charIndex = 0; charIndex < len; ++charIndex) 
                    grid[strIndex, charIndex] = strs[strIndex][charIndex];

            return grid;
        }

        /// <summary>
        /// Populates an array with a value
        /// Note that for reference types this will mean the same reference is used for every element
        /// </summary>
        /// <param name="arr">The array to fill</param>
        /// <param name="value">The value to fill the array with</param>
        public static void Fill<T>(this T[,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    arr[i,j] = value;
            
        }

        /// <summary>
        /// Populates an array with a value
        /// Note that for reference types this will mean the same reference is used for every element
        /// </summary>
        /// <param name="arr">The array to fill</param>
        /// <param name="value">The value to fill the array with</param>
        public static void Fill<T>(this T[,,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    for (int k = 0; k < arr.GetLength(2); k++)
                        arr[i, j, k] = value;

        }

        #endregion

        #region Char

        public static bool IsNumber(this char character)
        {
            return character is >= '0' and <= '9';
        }

        #endregion

        #region IEnumerable

        public static void PrintCollection(this IEnumerable items)
        {
            foreach (object item in items)
            {
                Console.WriteLine(item);
            }
        }

        public static void PrintCollectionCsv(this IEnumerable items)
        {
            foreach (object item in items)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
        }

        #endregion
    }
}
