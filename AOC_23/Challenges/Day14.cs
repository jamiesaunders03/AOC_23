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

        private readonly List<Obstruction>[] _columns;
        private readonly int _height;

        public Day14()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
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
            foreach (List<Obstruction> column in columns)
            {
                if (column[0].Type == 'O')
                    column[0].Position = new Vector2(column[0].Position.X, 0);
                for (int r = 1; r < column.Count; r++)
                {
                    if (column[r].Type == 'O')
                        column[r].Position = new Vector2(column[r].Position.X, column[r - 1].Position.Y + 1);
                }
            }

            long sum = 0;
            foreach (List<Obstruction> column in _columns)
            {
                sum += column
                    .Where(obs => obs.Type == 'O')
                    .Sum(itm => _height - itm.Position.Y);
            }
            return sum.ToString();  // 88059 too low, 111092 too high
        }

        public string Challenge2()
        {
            // move
            // transpose
            // store result
            // if seen again, work out interval
            // once found loop, determine final state
            // calculate score
            return "";
        }
    }
}
