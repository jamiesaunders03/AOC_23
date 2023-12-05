using System.Text.RegularExpressions;

using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day5 : IAocChallenge
    {
        internal readonly struct Transformation
        {
            public long ValueStart { get; }
            public long RangeStart { get; }
            public long RangeLength { get; }

            public Transformation(long valueStart, long rangeStart, long rangeLength)
            {
                ValueStart = valueStart;
                RangeStart = rangeStart;
                RangeLength = rangeLength;
            }
        }

        internal readonly struct Mapping
        {
            public string SourceType { get; }
            public string DestinationType { get; }
            public List<Transformation> Transformations { get; }

            public Mapping(string sourceType, string destinationType, List<Transformation> transformations)
            {
                SourceType = sourceType;
                DestinationType = destinationType;
                Transformations = transformations;
            }
        }

        internal readonly struct Range
        {
            public long Start { get; }
            public long Length { get; }

            public Range(long start, long length)
            {
                Start = start;
                Length = length;
            }
        }

        private static readonly Regex _seedsRe = new(@"seeds: ([\d ]+)");
        private static readonly Regex _sourceDestRe = new(@"(\w+)-to-(\w+) map:");

        private readonly List<long> _seeds;
        private readonly List<Mapping> _mappings;

        public int Day => 5;

        public Day5()
        {
            IEnumerator<string> input = new FetchData(Day).ReadInput().TrimEnd().Split('\n').Cast<string>().GetEnumerator();

            input.MoveNext();
            _seeds = GetSeeds(input);

            input.MoveNext();
            _mappings = GetMappings(input);
        }

        public string Challenge1()
        {
            // copy list
            var seeds = new List<long>(_seeds);

            foreach (Mapping map in _mappings)
            {
                seeds = seeds.Select(num => ApplyMap(num, map)).ToList();
            }

            return seeds.Min().ToString();
        }

        public string Challenge2()
        {
            var seeds = new List<Range>();
            for (int i = 0; i < _seeds.Count; i += 2)
                seeds.Add(new Range(_seeds[i], _seeds[i + 1]));

            foreach (Mapping map in _mappings)
            {
                // seeds = seeds.Select(num => ApplyMap(num, map)).ToList();
            }

            return seeds.Min().ToString();
        }

        private static long ApplyMap(long num, Mapping map)
        {
            foreach (Transformation t in map.Transformations)
            {
                if (t.RangeStart <= num && num < t.RangeStart + t.RangeLength)
                {
                    return t.ValueStart + (num - t.RangeStart);
                }
            }

            return num;
        }

        private static List<Range> ApplyMapToRange(Range r, Mapping map)
        {
            return null;
        }

        private static List<long> GetSeeds(IEnumerator<string> enumerator)
        {
            Match m = _seedsRe.Match(enumerator.Current);
            string seed = m.Groups[1].Value;
            return seed.Split(' ').Select(long.Parse).ToList();
        }

        private static List<Mapping> GetMappings(IEnumerator<string> enumerator)
        {
            List<Mapping> mappings = new();
            while (enumerator.MoveNext())
            {
                mappings.Add(GetMapping(enumerator));
            }

            return mappings;
        }

        private static Mapping GetMapping(IEnumerator<string> enumerator)
        {
            string header = enumerator.Current;
            Match m = _sourceDestRe.Match(header);
            string source = m.Groups[1].Value;
            string dest = m.Groups[2].Value;

            List<Transformation> transformations = new();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == "")
                    break;

                string[] sections = enumerator.Current.Split(' ');
                transformations.Add(new Transformation(
                    long.Parse(sections[0]),
                    long.Parse(sections[1]),
                    long.Parse(sections[2])));
            }

            return new Mapping(source, dest, transformations);
        }
    }
}
