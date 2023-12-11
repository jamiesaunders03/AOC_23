using AocHelper;
using AocHelper.Utilities;

namespace AOC_23.Challenges
{
    internal class Day10 : IAocChallenge
    {
        public struct Direction
        {
            public char Symbol { get; }
            public Vector2 Position { get; }

            public Direction(char symbol, Vector2 position)
            {
                Symbol = symbol;
                Position = position;
            }
        }

        private const byte NORTH = 0b1000;
        private const byte EAST = 0b0100;
        private const byte SOUTH = 0b0010;
        private const byte WEST = 0b0001;

        // NESW byte array
        private static readonly Dictionary<char, byte> _posConnections = new()
        {
            ['|'] = NORTH | SOUTH,
            ['-'] = EAST | WEST,
            ['L'] = NORTH | EAST,
            ['J'] = NORTH | WEST,
            ['7'] = SOUTH | WEST,
            ['F'] = SOUTH | EAST,
            ['.'] = 0b0000,
            ['S'] = NORTH | EAST | SOUTH | WEST,
        };

        private static readonly Dictionary<byte, byte> _opposite = new()
        {
            [NORTH] = SOUTH,
            [SOUTH] = NORTH,
            [EAST] = WEST,
            [WEST] = EAST,
        };

        // Top, Bottom. Left, Right
        private static readonly (Vector2, byte)[] _dirs =
        {
            (new Vector2(1, 0), EAST),
            (new Vector2(0, 1), SOUTH),
            (new Vector2(-1, 0), WEST),
            (new Vector2(0, -1), NORTH),
        };

        private readonly Direction[,] _directions;
        private readonly Vector2 _startPosition;

        public int Day => 10;

        public Day10()
        {
            string[] input = new FetchData(Day).ReadInput("Day10Part2d.txt").TrimEnd().Split('\n');
            _directions = new Direction[input.Length, input[0].Length];

            for (int row = 0; row < input.Length; row++)
                for (int col = 0; col < input[row].Length; col++)
                    _directions[row, col] = new Direction(input[row][col], new Vector2(col, row));

            _startPosition = _directions.ToEnumerable().First(d => d.Symbol == 'S').Position;
        }

        public string Challenge1()
        {
            int[,] mazePath = GetMazePath();

            int maxDist = mazePath.ToEnumerable().Count(x => x != int.MaxValue) / 2;
            return maxDist.ToString();
        }

        public string Challenge2()
        {
            int[,] mazePath = GetMazePath();
            int maxDiff = mazePath.ToEnumerable().Where(x => x != int.MaxValue).Max();
            int[,] groups = new int[mazePath.GetLength(0), mazePath.GetLength(1)];

            for (int i = 0; i < groups.GetLength(0); ++i)
            {
                bool inPipe = false;
                for (int j = 0; j < groups.GetLength(1); ++j)
                {
                    if (j < groups.GetLength(1) - 1 && (Math.Abs(mazePath[i, j] - mazePath[i, j + 1]) == 1 || Math.Abs(mazePath[i, j] - mazePath[i, j + 1]) == maxDiff))
                        inPipe = false;
                    else if (mazePath[i, j] != int.MaxValue)
                        inPipe = !inPipe;
                    else if (inPipe)
                        groups[i, j]++;
                }
            }

            Utilities.PrintGrid(groups, i => i == 1 ? '#' : '.');
            Console.WriteLine();

            for (int j = 0; j < groups.GetLength(1); ++j)
            {
                bool inPipe = false;
                for (int i = 0; i < groups.GetLength(0); ++i)
                {
                    if (i < groups.GetLength(0) - 1 && (Math.Abs(mazePath[i, j] - mazePath[i + 1, j]) == 1 || Math.Abs(mazePath[i, j] - mazePath[i + 1, j]) == maxDiff))
                        inPipe = false;
                    else if (mazePath[i, j] != int.MaxValue)
                        inPipe = !inPipe;
                    else if (inPipe)
                        groups[i, j]++;
                }
            }

            Utilities.PrintGrid(groups, i => i == 2 ? '#' : '.');
            int inside = groups.ToEnumerable().Count(x => x == 2);
            return inside.ToString();  // 2213 too high
        }

        /// <summary>
        /// Calculates the maze path as a multi dimensional bool array, with true representing the parts
        /// </summary>
        /// <returns></returns>
        private int[,] GetMazePath()
        {
            int[,] mazePath = new int[_directions.GetLength(0), _directions.GetLength(1)];
            mazePath.Fill(int.MaxValue);
            mazePath[_startPosition.Y, _startPosition.X] = 0;

            List<Vector2> reviewPositions = new() { _startPosition };
            while (reviewPositions.Count != 0)
            {
                Vector2 currentPos = reviewPositions[0];
                reviewPositions.RemoveAt(0);

                foreach ((Vector2, byte) dir in _dirs)
                {
                    Vector2 next = currentPos + dir.Item1;
                    if (IsConnected(currentPos, next, dir.Item2) && Utilities.VectorIndex(mazePath, next) == int.MaxValue)
                    {
                        mazePath[next.Y, next.X] = mazePath[currentPos.Y, currentPos.X] + 1;
                        reviewPositions.Add(next);
                        break;
                    }
                }
            }

            return mazePath;
        }

        /// <summary>
        /// Whether 2 vectors are connected in the given direction
        /// </summary>
        /// <param name="current">The current position</param>
        /// <param name="next">The direction to move to</param>
        /// <param name="direction">The direction to check (could be calculated but easier this way)</param>
        /// <returns></returns>
        private bool IsConnected(Vector2 current, Vector2 next, byte direction)
        {
            // Not in bounds
            if (next.X < 0 || next.Y < 0 || next.X >= _directions.GetLength(1) || next.Y >= _directions.GetLength(0))
                return false;

            Direction currentDir = Utilities.VectorIndex(_directions, current);
            Direction nextDir = Utilities.VectorIndex(_directions, next);

            byte currentSymbol = _posConnections[currentDir.Symbol];
            byte nextSymbol = _posConnections[nextDir.Symbol];
            byte oppDir = _opposite[direction];

            return ((currentSymbol & direction) != 0 && (nextSymbol & oppDir) != 0);
        }
    }
}
