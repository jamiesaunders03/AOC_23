using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day12 : IAocChallenge
    {
        public int Day => 12;

        private readonly List<string> _lines = new();
        private readonly List<int[]> _gaps = new();

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
                Console.WriteLine(sum.ToString());
            }

            return sum.ToString();  // 8210 too high
        }

        public string Challenge2()
        {
            throw new NotImplementedException();
        }

        public static int GetPermutations(string s, int[] nums)
        {
            if (nums.Length == 0)
                return s.Any(c => c == '#') ? 0 : 1;

            int total = 0;

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

            return total;
        }

        public static void AssertEq(int a, int b)
        {
            if (a != b)
                throw new Exception();
        }
    }
}
