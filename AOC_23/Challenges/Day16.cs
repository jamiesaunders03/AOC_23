using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_23.Challenges
{
    internal class Day16 : IAocChallenge
    {
        internal struct Visitor
        {
            public Vector2 Direction { get; set; }
            public Vector2 Position { get; set; }

            public Visitor(Vector2 direction, Vector2 position)
            {
                Direction = direction;
                Position = position;
            }
        }

        public int Day => 16;

        private readonly string[] _mirrors;

        public Day16()
        {
            _mirrors = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
        }

        public string Challenge1()
        {
            int energized = GetEnergizedTiles(new Visitor(Vector2.Right, new Vector2()));
            return energized.ToString();
        }

        public string Challenge2()
        {
            int maxEnergized = 0;
            int size = _mirrors.Length;
            for (int i = 0; i < size; ++i)
            {
                foreach (Visitor v in new Visitor[]
                    {
                        new(Vector2.Right, new Vector2(0, i)),
                        new(Vector2.Left, new Vector2(0, i)),
                        new(Vector2.Down, new Vector2(i, 0)),
                        new(Vector2.Up, new Vector2(i, 0)),
                    })
                {
                    int energized = GetEnergizedTiles(v);
                    maxEnergized = System.Math.Max(maxEnergized, energized);
                }
            }

            return maxEnergized.ToString();
        }

        private int GetEnergizedTiles(Visitor initialVisitor)
        {
            int size = _mirrors.Length;
            int[,] visited = new int[size, size];
            List<Visitor> beams = new() { initialVisitor, };

            while (beams.Count > 0)
            {
                int visitors = beams.Count;
                for (int i = 0; i < visitors; ++i)
                {
                    Visitor v = beams[0];
                    beams.RemoveAt(0);

                    if (!v.Position.InSpace(size, size) || (visited[v.Position.Y, v.Position.X] & GetOrientation(v.Direction)) != 0)
                        continue;

                    visited[v.Position.Y, v.Position.X] |= GetOrientation(v.Direction);
                    beams.AddRange(MoveBeam(v));
                }
            }

            return visited.ToEnumerable().Count(x => x != 0);
        }

        private List<Visitor> MoveBeam(Visitor v)
        {
            List<Visitor> newBeams = _mirrors[v.Position.Y][v.Position.X] switch
            {
                '.' => new List<Visitor> { new(v.Direction, v.Position + v.Direction) },
                '|' => SplitVector(v, _mirrors[v.Position.Y][v.Position.X]),
                '-' => SplitVector(v, _mirrors[v.Position.Y][v.Position.X]),
                '/' => new List<Visitor> { Reflect(v, inverse: true) },
                '\\' => new List<Visitor> { Reflect(v) },
                _ => throw new Exception(),
            };

            return newBeams;
        }

        private static List<Visitor> SplitVector(Visitor v, char splitter)
        {
            return splitter switch
            {
                '|' when v.Direction.X != 0 => new List<Visitor>
                {
                    new(Vector2.Up, v.Position + Vector2.Up),
                    new(Vector2.Down, v.Position + Vector2.Down),
                },
                '-' when v.Direction.Y != 0 => new List<Visitor>
                {
                    new(Vector2.Left, v.Position + Vector2.Left),
                    new(Vector2.Right, v.Position + Vector2.Right),
                },
                _ => new List<Visitor> { new(v.Direction, v.Position + v.Direction) },
            };
        }

        private static Visitor Reflect(Visitor v, bool inverse = false)
        {
            Vector2 vec = v.Direction;
            Vector2 newVec = new(vec.Y, vec.X);
            
            if (inverse) 
                newVec = -newVec;

            return new Visitor(newVec, v.Position + newVec);
        }

        private static int GetOrientation(Vector2 dir)
        {
            if (dir == Vector2.Up)
                return 1;
            if (dir == Vector2.Right)
                return 2;
            if (dir == Vector2.Down)
                return 4;
            if (dir == Vector2.Left)
                return 8;

            throw new Exception();
        }
    }
}
