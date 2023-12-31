using AocHelper;
using AocHelper.DataStructures;
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

            public Movement(Vector2 position, Vector2 direction, int steps)
            {
                Position = position;
                Direction = direction;
                Steps = steps;
            }
        }

        public int Day => 17;

        private readonly int[][] _heatMap;

        public Day17()
        {
            string[] input = new FetchData(Day).ReadInput("Day17Part1.txt").TrimEnd().Split('\n');
            _heatMap = input.Select(row => row.Select(num => num - '0').ToArray()).ToArray();
        }

        public string Challenge1()
        {
            int[,,,] input = new int[_heatMap.Length, _heatMap[0].Length, 3, 4];
            input.Fill(int.MaxValue);

            for (int i = 0; i < 4; ++i)
                SetPosition(ref input, (0, 0), 0, 0, i);

            Dijkstra(ref input, new Vector2());

            for (int i = 0; i < _heatMap.Length; i++)
            {
                for (int j = 0; j < _heatMap[i].Length; j++)
                {
                    Console.Write($"{MinAtPos(input, j, i):000} ");
                }
                Console.WriteLine();
            }
            
            return MinAtPos(input, input.GetLength(0) - 1, input.GetLength(1) - 1).ToString();  // 962 too low
        }

        public string Challenge2()
        {
            throw new NotImplementedException();
        }

        private void Dijkstra(ref int[,,,] input, Vector2 pos)
        {
            int width = _heatMap[0].Length;
            int height = _heatMap.Length;
            
            List<Movement> movements = new() { new Movement(pos, new Vector2(), 0) };

            while (movements.Count > 0)
            {
                Movement m = movements[0];
                movements.RemoveAt(0);

                Vector2[] adjacent = m.Position
                    .Adjacent()
                    .Where(v => v.InSpace(width, height))
                    .Where(v => v - m.Position != -m.Direction)
                    .ToArray();

                foreach (Vector2 move in adjacent)
                {
                    Vector2 direction = move - m.Position;
                    bool sameDirection = direction == m.Direction;
                    if (m.Steps == 2 && sameDirection)
                        continue;

                    int curDirIndex = GetIndexForDir(direction);
                    int prevDirIndex = GetIndexForDir(m.Direction);
                    int dist = input[m.Position.Y, m.Position.X, m.Steps, prevDirIndex] + _heatMap[move.Y][move.X];
                    if (sameDirection)
                    {
                        if (SetPosition(ref input, (move.X, move.Y), dist, m.Steps + 1, curDirIndex))
                            movements.Add(new Movement(move, move - m.Position, m.Steps + 1));
                    }
                    else
                    {
                        if (SetPosition(ref input, (move.X, move.Y), dist, 0, curDirIndex))
                            movements.Add(new Movement(move, move - m.Position, 0));
                    }
                }
            }
        }

        private bool SetPosition(ref int[,,,] dists, (int x, int y) pos, int dist, int depth, int dirIndex)
        {
            bool update = false;
            for (int i = depth; i <= 2; ++i)
            {
                if (dist < dists[pos.y, pos.x, i, dirIndex])
                {
                    dists[pos.y, pos.x, i, dirIndex] = dist;
                    update = true;
                }
            }

            return update;
        }

        private static int GetIndexForDir(Vector2 dir)
        {
            if (dir == Vector2.Up)
                return 0;
            else if (dir == Vector2.Right)
                return 1;
            else if (dir == Vector2.Down)
                return 2;
            else
                return 3;
        }

        private static int MinAtPos(in int[,,,] space, int x, int y)
        {
            int min = int.MaxValue;
            for (int dir = 0; dir < 4; ++dir)
            {
                if (space[y, x, 2, dir] < min)
                    min = space[y, x, 2, dir];
            }

            return min;
        }
    }
}
