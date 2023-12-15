using System.Text.RegularExpressions;
using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day15 : IAocChallenge
    {
        internal struct Lens
        {
            public string Code { get; }
            public int LensStrength { get; }

            public Lens(string code, int lensStrength)
            {
                Code = code;
                LensStrength = lensStrength;
            }
        }

        public int Day => 15;

        private const int MOD = 256;
        private static readonly Regex _labelRe = new(@"(\w+)(.*)");

        private readonly string[] _commands;

        public Day15()
        {
            _commands = new FetchData(Day).ReadInput().TrimEnd().Split(',');
        }

        public string Challenge1()
        {
            int total = _commands.Sum(GetHashScore);
            return total.ToString();
        }

        public string Challenge2()
        {
            var lens = new List<Lens>[MOD];
            for (int i = 0; i < MOD; i++)
            {
                lens[i] = new List<Lens>();
            }

            foreach (string s in _commands)
            {
                UpdateLens(s, ref lens);
            }

            var x = lens.SelectMany(s => s.Select(l => l.LensStrength)).ToHashSet();

            long score = 0;
            for (int box = 0; box < MOD; ++box)
            {
                for (int pos = 0; pos < lens[box].Count; ++pos)
                    score += (box + 1) * lens[box][pos].LensStrength * (pos + 1);
            }

            return score.ToString();  // 100911 too low
        }

        private static int GetHashScore(string s)
        {
            int hash = 0;

            foreach (char c in s)
            {
                hash += c;
                hash *= 17;
                hash %= MOD;
            }

            return hash;
        }

        private static void UpdateLens(string s, ref List<Lens>[] lens)
        {
            Match m = _labelRe.Match(s);

            string name = m.Groups[1].Value;
            string rest = m.Groups[2].Value;

            int index = GetHashScore(name);
            List<Lens> box = lens[index];
            int boxIndex = box.FindIndex(l => l.Code == name);

            if (rest == "-" && boxIndex != -1)
            {
                box.RemoveAt(boxIndex);
            }
            else if (rest != "-")
            {
                if (boxIndex == -1)
                {
                    box.Add(new Lens(name, rest[^1] - '0'));
                }
                else
                {
                    int lensStrength = rest[^1] - '0';
                    box[boxIndex] = new Lens(name, lensStrength);
                }
            }
        }
    }
}
