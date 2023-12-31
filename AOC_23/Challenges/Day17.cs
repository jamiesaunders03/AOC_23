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

        private const int DIRECTIONS = 4;
        private readonly int[][] _heatMap;

        public Day17()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
            _heatMap = input.Select(row => row.Select(num => num - '0').ToArray()).ToArray();
        }

        public string Challenge1()
        {
            Dictionary<int, int[,]> dists = Dijkstra(new Vector2(), 0, 3);
            return MinAtPos(dists, _heatMap.Length - 1, _heatMap[0].Length - 1).ToString();
        }

        public string Challenge2()
        {
            Dictionary<int, int[,]> dists = Dijkstra(new Vector2(), 3, 10);
            return MinAtPos(dists, _heatMap.Length - 1, _heatMap[0].Length - 1).ToString();
        }

        private Dictionary<int, int[,]> Dijkstra(Vector2 pos, int minDist, int maxDist)
        {
            int width = _heatMap[0].Length;
            int height = _heatMap.Length;

            Dictionary<int, int[,]> space = new();
            InitialiseSection(ref space, 0, maxDist, 0);

            List<Movement> movements = new() { new Movement(pos, Vector2.Right, 0), new Movement(pos, Vector2.Up, 0) };

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
                    if ((m.Steps < minDist && !sameDirection) || (m.Steps == maxDist - 1 && sameDirection))
                        continue;

                    int curDirIndex = GetIndexForDir(direction);
                    int prevDirIndex = GetIndexForDir(m.Direction);
                    int dist = GetDist(space, m.Position, m.Steps, prevDirIndex) + _heatMap[move.Y][move.X];
                    if (sameDirection)
                    {
                        if (SetPosition(ref space, (move.X, move.Y), dist, m.Steps + 1, curDirIndex, minDist, maxDist))
                            movements.Add(new Movement(move, move - m.Position, m.Steps + 1));
                    }
                    else
                    {
                        if (SetPosition(ref space, (move.X, move.Y), dist, 0, curDirIndex, minDist, maxDist))
                            movements.Add(new Movement(move, move - m.Position, 0));
                    }
                }
            }

            return space;
        }

        private bool SetPosition(ref Dictionary<int, int[,]> space, (int x, int y) pos, int dist, int depth, int dirIndex, int minDist, int maxDist)
        {
            bool update = false;
            int key = pos.y * _heatMap[0].Length + pos.x;

            if (!space.TryGetValue(key, out int[,] dists))
            {
                dists = InitialiseSection(ref space, key, maxDist, int.MaxValue);
            }

            if (depth < minDist)
            {
                if (dist < dists[depth, dirIndex])
                {
                    dists[depth, dirIndex] = dist;
                    update = true;
                }
            }
            else
            {
                for (int i = depth; i < dists.GetLength(0); ++i)
                {
                    if (dist < dists[i, dirIndex])
                    {
                        dists[i, dirIndex] = dist;
                        update = true;
                    }
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

        private int MinAtPos(in Dictionary<int, int[,]> space, int x, int y, int minHeight = 0)
        {
            int min = int.MaxValue;
            int[,] area = space[y * _heatMap[0].Length + x];

            for (int i = minHeight; i < area.GetLength(0); ++i)
                for (int dir = 0; dir < 4; ++dir)
                    if (area[i, dir] < min)
                        min = area[i, dir];
            
            return min;
        }

        private int GetDist(in Dictionary<int, int[,]> space, Vector2 pos, int depth, int dir)
        {
            if (space.TryGetValue(pos.Y * _heatMap[0].Length + pos.X, out int[,] moves))
            {
                return moves[depth, dir];
            }

            return int.MaxValue;
        }

        private static int[,] InitialiseSection(ref Dictionary<int, int[,]> map, int index, int length, int value)
        {
            int[,] shape = new int[length, DIRECTIONS];
            shape.Fill(value);

            map[index] = shape;
            return shape;
        }
    }
}
