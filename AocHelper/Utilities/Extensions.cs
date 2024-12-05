using System.Collections;

namespace AocHelper.Utilities
{
    public static class Extensions
    {
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
