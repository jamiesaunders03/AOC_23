using System.Collections;
using System.ComponentModel;
using System.Reflection;

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

        #region Enum

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
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
