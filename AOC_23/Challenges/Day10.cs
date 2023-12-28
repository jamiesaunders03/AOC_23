using AocHelper;
using AocHelper.DataStructures;
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
        private const byte HORIZONTAL = EAST | WEST;
        private const byte VERTICAL = NORTH | SOUTH;

        // NESW byte array
        private static readonly Dictionary<char, byte> _posConnections = new()
        {
            ['|'] = VERTICAL,
            ['-'] = HORIZONTAL,
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
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
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
            int[,] groups = new int[mazePath.GetLength(0), mazePath.GetLength(1)];

            for (int i = 0; i < groups.GetLength(0); ++i)
            {
                bool inPipe = false;
                int ridingPipe = 0;
                for (int j = 0; j < groups.GetLength(1); ++j)
                {
                    if (mazePath[i, j] == _posConnections['|'])  // Cross vertical pipe, invert
                    {
                        inPipe = !inPipe;
                    }  
                    else if (mazePath[i, j] != int.MaxValue && (mazePath[i, j] & VERTICAL) != 0) // On horizontal pipe, do nothing
                    {
                        if (ridingPipe == 0)
                        {
                            ridingPipe = mazePath[i, j] & VERTICAL;
                        }
                        else
                        {
                            if (ridingPipe != (mazePath[i, j] & VERTICAL))
                            {
                                inPipe = !inPipe;
                            }
                            ridingPipe = 0;
                        }
                    }
                    else if (inPipe && ridingPipe == 0)
                    {
                        groups[i, j]++;
                    }
                }
            }

            for (int j = 0; j < groups.GetLength(1); ++j)
            {
                bool inPipe = false;
                int ridingPipe = 0;
                for (int i = 0; i < groups.GetLength(0); ++i)
                {
                    if (mazePath[i, j] == _posConnections['-']) // Cross horizontal pipe, invert
                    {
                        inPipe = !inPipe;
                    }
                    else if (mazePath[i, j] != int.MaxValue && (mazePath[i, j] & HORIZONTAL) != 0)  // On horizontal pipe, do nothing
                    {
                        if (ridingPipe == 0)
                        {
                            ridingPipe = mazePath[i, j] & HORIZONTAL;
                        }
                        else
                        {
                            if (ridingPipe != (mazePath[i, j] & HORIZONTAL))
                                inPipe = !inPipe;
                            ridingPipe = 0;
                        }
                    }
                    else if (inPipe)
                    {
                        groups[i, j]++;
                    }
                }
            }

            int inside = groups.ToEnumerable().Count(x => x == 2);
            return inside.ToString();
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
                        mazePath[next.Y, next.X] = _posConnections[_directions[next.Y, next.X].Symbol];
                        reviewPositions.Add(next);
                        break;
                    }
                }
            }

            int startVal = 0;
            if (_startPosition.Y < 0 && mazePath[_startPosition.Y - 1, _startPosition.X] != int.MaxValue)
                startVal |= NORTH;
            if (_startPosition.X < _directions.GetLength(1) - 1 && mazePath[_startPosition.Y, _startPosition.X + 1] != int.MaxValue)
                startVal |= EAST;
            if (_startPosition.Y < _directions.GetLength(0) - 1 && mazePath[_startPosition.Y + 1, _startPosition.X] != int.MaxValue)
                startVal |= SOUTH;
            if (_startPosition.X > 0 && mazePath[_startPosition.Y, _startPosition.X - 1] != int.MaxValue)
                startVal |= WEST;

            mazePath[_startPosition.Y, _startPosition.X] = startVal;

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
