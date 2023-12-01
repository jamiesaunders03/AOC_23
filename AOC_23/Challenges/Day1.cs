using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day1 : IAocChallenge
    {
        private static readonly Dictionary<string, int> _intValues = new()
        {
            ["0"] = 0,
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9,
        };

        private static readonly Dictionary<string, int> _intStringValues = new(_intValues)
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
        };

        public void RunChallenge()
        {
            string[] input = new FetchData(1).ReadInput().TrimEnd().Split('\n');

            int part1 = Challenge1(input);
            int part2 = Challenge2(input);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        internal int Challenge1(string[] lines)
        {
            int total = 0;
            foreach (string line in lines)
            {
                int first = GetValue(line, _intValues, true);
                int last = GetValue(line, _intValues, false);

                total += first * 10 + last;
            }

            return total;
        }

        internal int Challenge2(string[] lines)
        {
            int total = 0;
            foreach (string line in lines)
            {
                int first = GetValue(line, _intStringValues, true);
                int last = GetValue(line, _intStringValues, false);

                total += first * 10 + last;
            }

            return total;
        }

        private static int GetValue(string seq, Dictionary<string, int> values, bool forwards)
        {
            int start = forwards ? 0 : seq.Length - 1;
            int stop = forwards ? seq.Length : -1;
            int step = forwards ? 1 : -1;

            Dictionary<string, int>.KeyCollection keys = values.Keys;

            for (int i = start; i != stop; i += step)
            {
                foreach (string option in keys)
                {
                    if (i + option.Length <= seq.Length && seq.Substring(i, option.Length) == option)
                    {
                        return values[option];
                    }
                }
            }

            throw new ArgumentException("Something went wrong");
        }
    }
}
