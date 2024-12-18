using AocHelper.DataStructures;

namespace AocHelper.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Indexes a 2d array using a vector
        /// </summary>
        /// <param name="arr">The array to index</param>
        /// <param name="vec">The vector to act as a index</param>
        /// <returns></returns>
        public static T VectorIndex<T>(T[,] arr, Vector2 vec)
        {
            return arr[vec.Y, vec.X];
        }
        
        /// <summary>
        /// Sets a value at an index of a 2d array using a vector
        /// </summary>
        /// <param name="arr">The array to index</param>
        /// <param name="vec">The vector to act as a index</param>
        /// <param name="value">The value to assign to that position</param>
        public static void SetVectorIndex<T>(T[,] arr, Vector2 vec, T value)
        {
            arr[vec.Y, vec.X] = value;
        }
        
        /// <summary>
        /// Indexes an array of strings
        /// </summary>
        /// <param name="strs">The string array to index</param>
        /// <param name="vec">The vector to act as a index</param>
        /// <returns></returns>
        public static char VectorIndex(string[] strs, Vector2 vec)
        {
            return strs[(int)vec.Y][(int)vec.X];
        }
        
        /// <summary>
        /// Indexes a 2d array using a vector
        /// </summary>
        /// <param name="arr">The array to index</param>
        /// <param name="vec">The vector to act as a index</param>
        /// <param name="val">The return value, or `default` if out of bounds</param>
        /// <returns></returns>
        public static bool TryVectorIndex<T>(T[,] arr, Vector2 vec, out T? val)
        {
            val = default;
            try
            {
                val = VectorIndex(arr, vec);
                return true;
            }
            catch (IndexOutOfRangeException) { }
            
            return false;
        }

        /// <summary>
        /// Swaps the given rows in the 2d array
        /// </summary>
        /// <param name="arr">The array to swap the rows of</param>
        /// <param name="r1">The index of the first row</param>
        /// <param name="r2">The index of the second row</param>
        public static void SwapRow<T>(T[,] arr, int r1, int r2)
        {
            if (r1 == r2)
                return;

            int cols = arr.GetLength(1);
            for (int i = 0; i < cols; ++i)
            {
                (arr[r1, i], arr[r2, i]) = (arr[r2, i], arr[r1, i]);
            }
        }
        
        /// <summary>
        /// Indexes an array of strings using a vector
        /// </summary>
        /// <param name="strs">The string array to index</param>
        /// <param name="vec">The vector to act as a index</param>
        /// <param name="val">The return value, or `default` if out of bounds</param>
        /// <returns></returns>
        public static bool TryVectorIndex(string[] strs, Vector2 vec, out char val)
        {
            val = default;
            try
            {
                val = VectorIndex(strs, vec);
                return true;
            }
            catch (IndexOutOfRangeException) { }
            
            return false;
        }

        public static T[] Initialize<T>(Func<T> initializer, int size)
        {
            var arr = new T[size];
            for (int i = 0; i < size; ++i)
            {
                arr[i] = initializer();
            }

            return arr;
        }
        
        public static T[,] Initialize<T>(Func<T> initializer, int sizex, int sizey)
        {
            var arr = new T[sizey, sizex];
            for (int y = 0; y < sizey; ++y)
                for (int x = 0; x < sizex; ++x)
                    arr[y, x] = initializer();
            

            return arr;
        }

        /// <summary>
        /// Prints a 2d array to the console
        /// </summary>
        /// <param name="vals">The array to print</param>
        /// <param name="map">How each elem should be displayed</param>
        public static void PrintGrid<T>(T[,] vals, Func<T, char> map)
        {
            for (int i = 0; i < vals.GetLength(0); ++i)
            {
                for (int j = 0; j < vals.GetLength(1); ++j)
                {
                    Console.Write(map(vals[i, j]));
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints a 2d array to the console
        /// </summary>
        /// <param name="vals">The array to print</param>
        public static void PrintGrid(char[,] vals)
        {
            for (int i = 0; i < vals.GetLength(0); ++i)
            {
                for (int j = 0; j < vals.GetLength(1); ++j)
                {
                    Console.Write(vals[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void PrintGrid<T>(IEnumerable<IEnumerable<T>> vals, Func<T, char> map)
        {
            foreach (IEnumerable<T> row in vals)
            {
                foreach (T val in row)
                {
                    Console.Write(map(val));
                }
                Console.WriteLine();
            }
        }
    }
}
