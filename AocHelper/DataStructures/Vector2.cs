using System.Text.RegularExpressions;

namespace AocHelper.DataStructures
{
    public class Vector2
    {
        /// <summary>
        /// The vectors X component
        /// </summary>
        public long X { get; }

        /// <summary>
        /// The vectors Y component
        /// </summary>
        public long Y { get; }

        /// <summary>
        /// Create a new vector at the given (x, y) co-ordinates
        /// </summary>
        public Vector2(long x, long y)
        {
            X = x;
            Y = y;
        }
        
        /// <summary>
        /// Copy constructor, constructs a vector at the same point as the original
        /// </summary>
        /// <param name="v">The vector to base this vectors position off</param>
        public Vector2(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }

        /// <summary>
        /// Constructs a Vector2 t the point (0, 0)
        /// </summary>
        public Vector2() : this(0, 0) { }

        /// <summary>
        /// Factory method to create a new instance of a vector 2 object from a regex match.
        /// This method assumes that the matches groups 1 & 2 represent the x & y positions of
        /// the vector as parseable ints
        /// </summary>
        /// <param name="m">The match object to parse</param>
        public static Vector2 FromMatch(Match m)
        {
            int first = int.Parse(m.Groups[1].Value);
            int second = int.Parse(m.Groups[2].Value);

            return new Vector2(first, second);
        }

        #region Defaults

        public static Vector2 Right => new(1, 0);
        public static Vector2 Left => new(-1, 0);
        public static Vector2 Up => new(0, 1);
        public static Vector2 Down => new(0, -1);

        /// <summary>
        /// All directions from the origin with maximum magnitude 1 in each axis
        /// </summary>
        public static Vector2[] Directions => new[]
        {
            Up,
            Up + Right,
            Right,
            Down + Right,
            Down,
            Down + Left,
            Left,
            Up + Left,
        };

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
        
        public static Vector2 operator *(Vector2 v, long scale)
        {
            return new Vector2(v.X * scale, v.Y * scale);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
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

        #region Overrides

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Vector2 v && X == v.X && Y == v.Y;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return (X.GetHashCode() * Y.GetHashCode()).GetHashCode();
        }

        #endregion
        
        #region Distance Based

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
        public long Manhattan(Vector2 v)
        {
            return Math.Abs(X - v.X) + Math.Abs(Y - v.Y);
        }

        /// <summary>
        /// Returns the greatest 1 dimensional distance between the 2 vectors
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public long MaxDimDistance(Vector2 v)
        {
            return Math.Max(Math.Abs(X - v.X), Math.Abs(Y - v.Y));
        }
        
        #endregion Distance Based

        #region Rotational

        /// <summary>
        /// Returns the current Vector rotated left 90 degrees
        /// </summary>
        public Vector2 RotateLeft()
        {
            return new Vector2(-Y, X);
        }
        
        /// <summary>
        /// Returns the current Vector rotated right 90 degrees
        /// </summary>
        public Vector2 RotateRight()
        {
            return new Vector2(Y, -X);
        }

        /// <summary>
        /// Returns the vector normal to this one with the same magnitude
        /// </summary>
        /// <returns></returns>
        public Vector2 Normal()
        {
            return new Vector2(-Y, X);
        }

        #endregion Rotational

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

        #region Adjacency

        /// <summary>
        /// Returns all the vectors that are directly adjacent to this one, excluding ones diagonally adjacent
        /// </summary>
        /// <returns></returns>
        public Vector2[] Adjacent()
        {
            return new[]
            {
                this + Up,
                this + Right,
                this + Down,
                this + Left,
            };
        }
        
        /// <summary>
        /// Returns all the vectors that are directly adjacent to this one, including ones diagonally adjacent
        /// </summary>
        /// <returns></returns>
        public Vector2[] Surrounding()
        {
            return new[]
            {
                this + Up,
                this + Up + Right,
                this + Right,
                this + Down + Right,
                this + Down,
                this + Down + Left,
                this + Left,
                this + Up + Left,
            };
        }
        
        /// <summary>
        /// Returns all the vectors that are within `range` of the current vector
        /// This is vectors with values `Vector2([x - range, x + range], [y - range, y + range])` exclusing the
        /// current vector.
        /// </summary>
        /// <param name="range">The range of tiles to search</param>
        /// <returns></returns>
        public ICollection<Vector2> Surrounding(int range)
        {
            if (range <= 0)
                return [];

            List<Vector2> points = [];

            for (int i = -range; i <= range; ++i)
                for (int j = -range; j <= range; ++j)
                    if (i != 0 || j != 0)
                        points.Add(new Vector2(i + X, j + Y));
            
            return points;
        }
        
        /// <summary>
        /// Returns all the vectors that are within Manhatten distance `range` of the current vector
        /// This excludes the current vector.
        /// </summary>
        /// <param name="range">The range of tiles to search</param>
        /// <returns></returns>
        public ICollection<Vector2> Nearby(int range)
        {
            return Surrounding(range)
                .Where(v => v.Manhattan(this) <= range)
                .ToList();
        }

        #endregion
    }
}
