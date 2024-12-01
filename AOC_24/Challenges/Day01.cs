using AocHelper;
using System.Text.RegularExpressions;

namespace AOC_24.Challenges
{
    internal class Day01 : IAocChallenge
    {
        private List<int> _first;
        private List<int> _second;

        private static readonly Regex _groupsRe = new(@"(\d+)\s+(\d+)");

        public int Day => 1;

        public Day01()
        {
            string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');

            _first = new List<int>();
            _second = new List<int>();

            foreach (string line in input)
            {
                GroupCollection parts = _groupsRe.Match(line).Groups;
                _first.Add(int.Parse(parts[1].Value));
                _second.Add(int.Parse(parts[2].Value));
            }
        }

        public string Challenge1()
        {
            int diff_total = 0;

            List<int> first = _first.ToList();
            List<int> second = _second.ToList();

            first.Sort();
            second.Sort();

            for (int i = 0; i < first.Count; ++i)
            {
                diff_total += Math.Abs(first[i] - second[i]);
            }

            return diff_total.ToString();
        }

        public string Challenge2()
        {
            Dictionary<int, int> secondOccurrences = new();
            foreach (int num in _second)
            {
                secondOccurrences[num] = secondOccurrences.GetValueOrDefault(num, 0) + 1;
            }

            int simScore = _first.Sum(num => num * secondOccurrences.GetValueOrDefault(num, 0));
            return simScore.ToString();
        }
    }
}
