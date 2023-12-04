
namespace AocHelper.Utilities
{
    public class Vector2
    {
        /// <summary>
        /// The vectors X component
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The vectors Y component
        /// </summary>
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x; 
            Y = y;
        }

        #region Operations

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        #endregion

        /// <summary>
        /// Gets the magnitude of this vector
        /// </summary>
        /// <returns></returns>
        public double Abs()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        /// <summary>
        /// The euclidean dist between the 2 vectors
        /// </summary>
        /// <param name="v">The vector to get the distance to</param>
        /// <returns></returns>
        public double Euclidean(Vector2 v)
        {
            return Math.Sqrt(Math.Pow(X - v.X, 2) + Math.Pow(Y - v.Y, 2));
        }

        /// <summary>
        /// The Manhattan distance between the 2 vectors
        /// </summary>
        /// <param name="v">The vector to get the distance to</param>
        /// <returns></returns>
        public int Manhattan(Vector2 v)
        {
            return Math.Abs(X - v.X) + Math.Abs(Y - v.Y);
        }

        /// <summary>
        /// Returns the greatest 1 dimensional distance between the 2 vectors
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int MaxDimDistance(Vector2 v)
        {
            return Math.Max(Math.Abs(X - v.X), Math.Abs(Y - v.Y));
        }

        /// <summary>
        /// Returns the vector normal to this one with the same magnitude
        /// </summary>
        /// <returns></returns>
        public Vector2 Normal()
        {
            return new Vector2(-Y, X);
        }
    }
}
