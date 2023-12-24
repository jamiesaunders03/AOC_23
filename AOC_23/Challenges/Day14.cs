using System.Collections.Immutable;
using AocHelper;
using AocHelper.Utilities;

namespace AOC_23.Challenges
{
    internal class Day14 : IAocChallenge
    {
        internal class Obstruction
        {
            public char Type { get; }
            public Vector2 Position { get; set; }

            public Obstruction(char type, Vector2 position)
            {
                Type = type;
                Position = position;
            }
        }

        public int Day => 14;

        private const int TOTAL_ITERATIONS = 1_000_000_000;

        private readonly List<Obstruction>[] _columns;
        private readonly int _height;

        public Day14()
        {
            string[] input = new FetchData(Day).ReadInput("Day14Part1.txt").TrimEnd().Split('\n');
            _height = input.Length;
            _columns = new List<Obstruction>[input[0].Length];

            for (int i = 0; i < _columns.Length; i++)
            {
                _columns[i] = new List<Obstruction>();
            }

            foreach (((int i, int j), char c) in Enumeration.EnumerateArray(input)) 
            {
                if (c != '.')
                    _columns[j].Add(new Obstruction(c, new Vector2(j, i)));
            }
        }

        public string Challenge1()
        {
            // Copy references
            List<Obstruction>[] columns = _columns.Select(lst => lst.ToList()).ToArray();
            MoveVertical(ref columns, invert:false);

            long sum = CalculateLoad(columns);
            return sum.ToString();
        }

        public string Challenge2()
        {
            List<Obstruction>[] columns = _columns.Select(lst => lst.ToList()).ToArray();
            Dictionary<HashContainer<int>, int> prevPositions = new();
            int iteration = 0;

            while (iteration < 1_000_000_000)
            {
                bool isVertical = iteration % 2 == 0;
                bool invert = iteration % 4 is 1 or 2;
                columns = GetGroups(columns, isVertical, invert);

                if (isVertical)
                    MoveVertical(ref columns, invert);
                else
                    MoveHorizontal(ref columns, invert);

                HashContainer<int> positions = GetPositionSet(columns);
                if (prevPositions.TryGetValue(positions, out int prevIteration))
                {
                    int toGo = TOTAL_ITERATIONS - iteration;
                    int cycleLen = iteration - prevIteration;
                    int fullCycles = toGo / cycleLen;
                    iteration += fullCycles * cycleLen;
                }
                else
                {
                    prevPositions[positions] = iteration;
                }
                ++iteration;
            }

            long sum = CalculateLoad(columns);
            return sum.ToString();  // 84110 too low
        }

        private long CalculateLoad(List<Obstruction>[] columns)
        {
            long sum = 0;
            foreach (List<Obstruction> column in columns)
            {
                sum += column
                    .Where(obs => obs.Type == 'O')
                    .Sum(itm => _height - itm.Position.Y);
            }

            return sum;
        }

        private List<Obstruction>[] GetGroups(List<Obstruction>[] obstructions, bool isVertical, bool invert)
        {
            Func<Obstruction, int> selector;
            Comparison<Obstruction> comparitor;
            if (isVertical)
            {
                selector = obstruction => obstruction.Position.X;
                comparitor = (obs1, obs2) => obs1.Position.Y.CompareTo(obs2.Position.Y);
            }
            else
            {
                selector = obstruction => obstruction.Position.Y;
                comparitor = (obs1, obs2) => obs1.Position.X.CompareTo(obs2.Position.X);
            }

            List<Obstruction>[] newObstructions = obstructions
                .SelectMany(elem => elem)
                .GroupBy(selector)
                .Select(lst => lst.ToList())
                .ToArray();

            foreach (List<Obstruction> obs in newObstructions)
            {
                obs.Sort(comparitor);
                if (invert)
                    obs.Reverse();
            }

            return newObstructions;
        }

        private HashContainer<int> GetPositionSet(List<Obstruction>[] columns)
        {
            var vals = columns
                .SelectMany(col => col
                    .Where(obs => obs.Type == 'O')
                    .Select(obs => obs.Position.X + obs.Position.Y * _height))
                .ToList();
            vals.Sort();

            return new HashContainer<int>(vals);
        }

        private void MoveVertical(ref List<Obstruction>[] columns, bool invert)
        {
            int end = invert ? _height - 1 : 0;
            int step = invert ? -1 : 1;

            foreach (List<Obstruction> column in columns)
            {
                if (column[0].Type == 'O')
                    column[0].Position = new Vector2(column[0].Position.X, end);
                for (int r = 1; r < column.Count; r++)
                {
                    if (column[r].Type == 'O')
                        column[r].Position = new Vector2(column[r].Position.X, column[r - 1].Position.Y + step);
                }
            }
        }

        private void MoveHorizontal(ref List<Obstruction>[] rows, bool invert)
        {
            int firstSlot = invert ? _height - 1 : 0;
            int step = invert ? -1 : 1;

            foreach (List<Obstruction> row in rows)
            {
                if (row[0].Type == 'O')
                    row[0].Position = new Vector2(firstSlot, row[0].Position.Y);
                for (int r = 1; r < row.Count; r++)
                {
                    if (row[r].Type == 'O')
                        row[r].Position = new Vector2(row[r - 1].Position.X + step, row[r].Position.Y);
                }
            }
        }
    }
}
