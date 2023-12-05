using System.Text.RegularExpressions;

using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day5 : IAocChallenge
    {
        internal readonly struct Transformation
        {
            public long Dest { get; }
            public long Start { get; }
            public long Length { get; }

            public Transformation(long dest, long start, long length)
            {
                Dest = dest;
                Start = start;
                Length = length;
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
                var newSeeds = new List<Range>();
                foreach (Range seedRange in seeds)
                {
                    newSeeds.AddRange(ApplyMapToRange(seedRange, map));
                }

                seeds = newSeeds;
            }

            return seeds.Min(r => r.Start).ToString();
        }

        private static long ApplyMap(long num, Mapping map)
        {
            foreach (Transformation t in map.Transformations)
            {
                if (t.Start <= num && num < t.Start + t.Length)
                {
                    return t.Dest + (num - t.Start);
                }
            }

            return num;
        }

        private static List<Range> ApplyMapToRange(Range r, Mapping map)
        {
            List<Range> newSeeds = new();
            // Areas that overlap, used to work out what ranges are unmapped and therefor unchanged after all other mapping are applied
            List<Range> intersections = new();
            foreach (Transformation t in map.Transformations)
            {
                // Ranges have no overlap
                if (r.Start > t.Start + t.Length || t.Start > r.Start + r.Length)  
                {
                    continue;
                }

                // Get overlapping range
                long rangeStart = Math.Max(r.Start, t.Start);
                long rangeEnd = Math.Min(r.Start + r.Length, t.Start + t.Length);
                long length = rangeEnd - rangeStart;

                // How much to shift range by
                long offset = t.Dest - t.Start;
                newSeeds.Add(new Range(rangeStart + offset, length));
                intersections.Add(new Range(rangeStart, length));
            }

            // Sort intersections to iterate through and fid the gaps between
            intersections.Sort((r1, r2) => r1.Start.CompareTo(r2.Start));
            
            long current = r.Start;
            long end = r.Start + r.Length - 1;
            int index = 0;

            // Leading part of range and gaps between mapped areas
            while (current < end && index < intersections.Count)
            {
                if (current < intersections[index].Start)
                {
                    long range = intersections[index].Start - current;
                    newSeeds.Add(new Range(current, range));
                }
                current = intersections[index].Start + intersections[index].Length;
                ++index;
            }

            // Account for any trailing unmapped part of the range
            if (current < end)
            {
                newSeeds.Add(new Range(current, end - current + 1));
            }

            return newSeeds;
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
