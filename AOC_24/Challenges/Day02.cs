using AocHelper;

namespace AOC_24.Challenges
{
    internal class Day02 : IAocChallenge
    {
        public int Day => 2;

        private readonly List<List<int>> _levels;

        public Day02()
        {
            string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
            _levels = input.Select(line => line.Split(' ').Select(int.Parse).ToList()).ToList();
        }

        public string Challenge1()
        {
            int safeReports = _levels
                .Cast<ICollection<int>>()
                .Count(report => Increasing(report) || Increasing(report.Reverse().ToArray()));

            return safeReports.ToString();
        }

        public string Challenge2()
        {
            int safeReports = _levels
                .Cast<ICollection<int>>()
                .Count(report => IncreasingWithTol(report) || IncreasingWithTol(report.Reverse().ToArray()));

            return safeReports.ToString();
        }

        private static bool Increasing(ICollection<int> report, int minStep = 1, int maxStep = 3)
        {
            int len = report.Count;
            for (int i = 0; i < len - 1; ++i)
            {
                int diff = report.ElementAt(i + 1) - report.ElementAt(i);

                if (diff < minStep || diff > maxStep)
                    return false;
            }

            return true;
        }
        
        private static bool IncreasingWithTol(ICollection<int> report, int minStep = 1, int maxStep = 3)
        {
            if (Increasing(report, minStep, maxStep))
                return true;
            
            int len = report.Count;
            for (int i = 0; i < len; ++i)
            {
                IEnumerable<int> collection = report.Take(i);
                int[] arr = collection.Concat(report.Skip(i + 1)).ToArray();
                if (Increasing(arr, minStep, maxStep))
                    return true;
            }

            return false;
        }
    }
}