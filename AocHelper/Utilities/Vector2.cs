
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

        public Vector2() : this(0, 0) { }

        #region Defaults

        public static Vector2 Right => new (1, 0);
        public static Vector2 Left => new (-1, 0);
        public static Vector2 Up => new (0, 1);
        public static Vector2 Down => new (0, -1);

        #endregion

        #region Operations

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator -(Vector2 v1)
        {
            return new Vector2(-v1.X, -v1.Y);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        #endregion

        /// <summary>
        /// Gets the magnitude of this vector
        /// </summary>
        /// <returns></returns>
        public double Abs()
        {
            return System.Math.Sqrt(System.Math.Pow(X, 2) + System.Math.Pow(Y, 2));
        }

        /// <summary>
        /// The euclidean dist between the 2 vectors
        /// </summary>
        /// <param name="v">The vector to get the distance to</param>
        /// <returns></returns>
        public double Euclidean(Vector2 v)
        {
            return System.Math.Sqrt(System.Math.Pow(X - v.X, 2) + System.Math.Pow(Y - v.Y, 2));
        }

        /// <summary>
        /// The Manhattan distance between the 2 vectors
        /// </summary>
        /// <param name="v">The vector to get the distance to</param>
        /// <returns></returns>
        public int Manhattan(Vector2 v)
        {
            return System.Math.Abs(X - v.X) + System.Math.Abs(Y - v.Y);
        }

        /// <summary>
        /// Returns the greatest 1 dimensional distance between the 2 vectors
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int MaxDimDistance(Vector2 v)
        {
            return System.Math.Max(System.Math.Abs(X - v.X), System.Math.Abs(Y - v.Y));
        }

        /// <summary>
        /// Returns the vector normal to this one with the same magnitude
        /// </summary>
        /// <returns></returns>
        public Vector2 Normal()
        {
            return new Vector2(-Y, X);
        }

        /// <summary>
        /// Checks whether the current vector fits within a space of the given dimensions
        /// </summary>
        /// <param name="width">The width of the space</param>
        /// <param name="height">The height of the space</param>
        /// <returns></returns>
        public bool InSpace(int width, int height)
        {
            return X >= 0 && X < width && Y >= 0 && Y < height;
        }
    }
}
