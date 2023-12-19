using AocHelper;
using AocHelper.Utilities;

namespace AOC_23.Challenges
{
    internal class Day17 : IAocChallenge
    {
        internal struct Movement
        {
            public Vector2 Position { get; }
            public Vector2 Direction { get; }
            public int Steps { get; }
            public int CurrentHeat { get; }

            public Movement(Vector2 position, Vector2 direction, int steps, int currentHeat)
            {
                Position = position;
                Direction = direction;
                Steps = steps;
                CurrentHeat = currentHeat;
            }
        }

        public int Day => 17;

        private readonly int[][] _heatMap;

        public Day17()
        {
            string[] input = new FetchData(Day).ReadInput("Day17Part1a.txt").TrimEnd().Split('\n');
            _heatMap = input.Select(row => row.Select(num => num - '0').ToArray()).ToArray();
        }

        public string Challenge1()
        {
            int[,,] input = new int[_heatMap.Length, _heatMap[0].Length, 3];
            input.Fill(int.MaxValue);
            SetPosition(ref input, (0, 0), 0, 0);

            Dijkstra(ref input, new Vector2());

            for (int k = 0; k < 3; ++k)
            {
                for (int i = 0; i < _heatMap.Length; i++)
                {
                    for (int j = 0; j < _heatMap[i].Length; j++)
                    {
                        Console.Write($"{input[i, j, k]:000} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            

            return input[input.GetLength(0) - 1, input.GetLength(1) - 1, 2].ToString();  // 962 too low
        }

        public string Challenge2()
        {
            throw new NotImplementedException();
        }

        private void Dijkstra(ref int[,,] input, Vector2 pos)
        {
            int width = _heatMap[0].Length;
            int height = _heatMap.Length;
            
            List<Movement> movements = new() { new Movement(pos, new Vector2(), 0, 0) };

            while (movements.Count > 0)
            {
                Movement m = movements[0];
                movements.RemoveAt(0);

                Vector2[] adjacent = m.Position
                    .Adjacent()
                    .Where(v => v.InSpace(width, height))
                    .Where(v => v != -m.Direction)
                    .ToArray();

                foreach (Vector2 move in adjacent)
                {
                    Vector2 direction = move - m.Position;
                    bool sameDirection = direction == m.Direction;
                    if (m.Steps == 2 && sameDirection)
                        continue;

                    int dist = m.CurrentHeat + _heatMap[move.Y][move.X];
                    if (sameDirection)
                    {
                        if (SetPosition(ref input, (move.X, move.Y), dist, m.Steps + 1))
                            movements.Add(new Movement(move, move - m.Position, m.Steps + 1, input[move.Y, move.X, m.Steps + 1]));
                    }
                    else
                    {
                        if (SetPosition(ref input, (move.X, move.Y), dist, 0))
                            movements.Add(new Movement(move, move - m.Position, 0, input[move.Y, move.X, 0]));
                    }
                }
            }
        }

        private bool SetPosition(ref int[,,] dists, (int x, int y) pos, int dist, int depth)
        {
            bool update = false;
            for (int i = depth; i <= 2; ++i)
            {
                if (dist < dists[pos.y, pos.x, i])
                {
                    dists[pos.y, pos.x, i] = dist;
                    update = true;
                }
            }

            return update;
        }
    }
}
