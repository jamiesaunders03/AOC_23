using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day12 : IAocChallenge
    {
        public int Day => 12;

        private readonly List<string> _lines = new();
        private readonly List<int[]> _gaps = new();

        // Cache output for speed
        private static readonly Dictionary<string, long> _cache = new();

        public Day12()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');

            foreach (string line in input)
            {
                int split = line.IndexOf(' ');
                _lines.Add(line[..split]);

                string[] nums = line[(split + 1)..].Split(',');
                _gaps.Add(nums.Select(int.Parse).ToArray());
            }
        }

        public string Challenge1()
        {
            long sum = 0;
            for (int i = 0; i < _lines.Count; ++i)
            {
                sum += GetPermutations(_lines[i], _gaps[i]);
            }

            return sum.ToString();
        }

        public string Challenge2()
        {
            long sum = 0;
            for (int i = 0; i < _lines.Count; ++i)
            {
                string line = string.Join("?", Enumerable.Repeat(_lines[i], 5));
                int[] nums = Enumerable.Repeat(_gaps[i], 5).SelectMany(arr => arr).ToArray();

                sum += GetPermutations(line, nums);
                _cache.Clear();
            }

            return sum.ToString();  // 37941262278587 too low
        }

        public static long GetPermutations(string s, int[] nums)
        {
            if (nums.Length == 0)
                return s.Any(c => c == '#') ? 0 : 1;

            if (_cache.TryGetValue(FormatKey(s, nums), out long hit))
            {
                return hit;
            }

            long total = 0;

            int maxIterStop = s.Length - (nums.Sum() + nums.Length - 1);
            for (int i = 0; i <= maxIterStop; ++i)
            {
                IEnumerable<char> sFrame = s.Skip(i);
                if (sFrame.Take(nums[0]).All(c => c is '#' or '?') && sFrame.Skip(nums[0]).Take(1).All(c => c != '#'))
                {
                    total += GetPermutations(string.Concat(sFrame.Skip(nums[0] + 1)), nums.Skip(1).ToArray());
                }

                if (s[i] == '#')
                    break;
            }

            CacheAdd(s, nums, total);
            return total;
        }

        private static void CacheAdd(string s, int[] nums, long value)
        {
            _cache[FormatKey(s, nums)] = value;
        }

        private static string FormatKey(string s, int[] nums)
        {
            string numString = string.Join(",", nums);
            return s + numString;
        }
    }
}
