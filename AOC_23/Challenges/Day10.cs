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
        private static (Vector2, byte)[] _dirs =
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
            int[,] distances = new int[_directions.GetLength(0), _directions.GetLength(1)];
            distances.Fill(int.MaxValue);
            distances[_startPosition.Y, _startPosition.X] = 0;

            List<Vector2> reviewPositions = new() { _startPosition };
            while (reviewPositions.Count != 0)
            {
                Vector2 currentPos = reviewPositions[0];
                int currentVal = distances[currentPos.Y, currentPos.X];
                reviewPositions.RemoveAt(0);

                foreach ((Vector2, byte) dir in _dirs)
                {
                    Vector2 next = currentPos + dir.Item1;
                    if (Connected(currentPos, next, dir.Item2) && distances[next.Y, next.X] > currentVal)
                    {
                        distances[next.Y, next.X] = currentVal + 1;
                        reviewPositions.Add(next);
                    }
                }
            }

            int maxDist = distances.ToEnumerable().Where(n => n != int.MaxValue).Max();
            return maxDist.ToString();
        }

        public string Challenge2()
        {
            throw new NotImplementedException();
        }

        private bool Connected(Vector2 current, Vector2 next, byte direction)
        {
            // Not in bounds
            if (next.X < 0 || next.Y < 0 || next.X >= _directions.GetLength(1) || next.Y >= _directions.GetLength(0))
                return false;

            Direction currentDir = _directions[current.Y, current.X];
            Direction nextDir = _directions[next.Y, next.X];

            byte currentSymbol = _posConnections[currentDir.Symbol];
            byte nextSymbol = _posConnections[nextDir.Symbol];
            byte oppDir = _opposite[direction];

            return ((currentSymbol & direction) != 0 && (nextSymbol & oppDir) != 0);
        }
    }
}
