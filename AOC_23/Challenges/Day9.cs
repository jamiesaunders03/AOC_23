using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day9 : IAocChallenge
    {
        public int Day => 9;

        private readonly List<int>[] _rows;

        public Day9()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
            _rows = input
                .Select(s => s.Split(' ').Select(int.Parse).ToList())
                .ToArray();
        }

        public string Challenge1()
        {
            long total = _rows.Aggregate(0L, (current, nums) => current + GetNextNum(nums));
            return total.ToString();
        }

        public string Challenge2()
        {
            List<int>[] otherRows = {
                new() { 10, 13, 16, 21, 30, 45, },
                new() { 0, 3, 6, 9, 12, 15, },
                new() { 1, 3, 6, 10, 15, 21, },
            };
            long l = otherRows.Aggregate(0L, (current, nums) => current + GetPrevNum(nums));

            long total = _rows.Aggregate(0L, (current, nums) => current + GetPrevNum(nums));
            return total.ToString();
        }

        private static long GetNextNum(List<int> row)
        {
            List<List<int>> diffs = GetDiffs(row);
            return diffs.Sum(r => r[^1]);
        }

        private static long GetPrevNum(List<int> row)
        {
            List<List<int>> diffs = GetDiffs(row);
            return diffs.Aggregate(0L, (current, r) => r[0] - current);
        }

        private static List<List<int>> GetDiffs(List<int> row)
        {
            List<List<int>> diffs = new() { row.ToList() };
            List<int> prev = diffs[^1];
            while (prev.Any(n => n != 0))
            {
                List<int> newRow = new(prev.Count - 1);
                for (int i = 0; i < prev.Count - 1; i++)
                {
                    newRow.Add(prev[i + 1] - prev[i]);
                }
                diffs.Add(newRow);
                prev = newRow;
            }

            return diffs;
        }
    }
}
