using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace AocHelper.Utilities
{
    public static class Extensions
    {
        #region Array

        /// <summary>
        /// Populates an array with a value
        /// Note that for reference types this will mean the same reference is used for every element
        /// </summary>
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
        /// Populates an array with a value
        /// Note that for reference types this will mean the same reference is used for every element
        /// </summary>
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
        /// <param name="value">The value to fill the array with</param>
        public static void Fill<T>(this T[,,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    for (int k = 0; k < arr.GetLength(2); k++)
                        arr[i, j, k] = value;

        }

        /// <summary>
        /// Populates an array with a value
        /// Note that for reference types this will mean the same reference is used for every element
        /// </summary>
        /// <param name="value">The value to fill the array with</param>
        public static void Fill<T>(this T[,,,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            for (int j = 0; j < arr.GetLength(1); j++)
            for (int k = 0; k < arr.GetLength(2); k++)
            for (int l = 0; l < arr.GetLength(3); l++)
                arr[i, j, k, l] = value;

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
