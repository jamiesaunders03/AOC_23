
using AocHelper;
using NLog;
using Range = AocHelper.DataStructures.Range;

namespace AOC_25.Challenges;

public class Day05 : IAocChallenge
{
    #region Boilerplate

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public int Day => 5;

    #endregion

    private readonly List<Range> _ranges = [];
    private readonly List<long> _ids = []; 

    public Day05()
    {
        _logger.Info($"Constructing {GetType().Name} Solver");

        string text = """
            3-5
            10-14
            16-20
            12-18

            1
            5
            8
            11
            17
            32
            """;
        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split("\n\n");
        
        // Ranges
        foreach (string range in input[0].Split('\n'))
        {
            string[] parts = range.Split('-');
            long start = long.Parse(parts[0]);
            _ranges.Add(new Range(start, long.Parse(parts[1]) - start + 1));
        }

        // IDs
        foreach (string item in input[1].Split('\n'))
        {
            _ids.Add(long.Parse(item));
        }
        
        // sort in range end ascending
        _ranges.Sort((r1, r2) => r1.End.CompareTo(r2.End));
    }

    /// <summary>
    /// Which numbers are in any ranges
    /// </summary>
    public string Challenge1()
    {
        _logger.Info($"Starting {GetType().Name} Part 1");

        List<long> freshIds = [];

        foreach (long id in _ids)
        {
            bool fresh = false;

            int i = 0;
            while (i < _ranges.Count)
            {
                if (_ranges[i].Contains(id))
                {
                    fresh = true;
                    break;
                }

                ++i;
            }
            
            if (fresh)
                freshIds.Add(id);
        }

        return freshIds.Count.ToString();
    }

    /// <summary>
    /// How many numbers across all ranges
    /// </summary>
    public string Challenge2()
    {
        _logger.Info($"Starting {GetType().Name} Part 2");
        
        HashSet<Range> toRemove;
        HashSet<Range> ranges = _ranges.ToHashSet();
        do
        {
            toRemove = [];
            HashSet<Range> createdRanges = [];

            for (int i = 0; i < ranges.Count; ++i)
            {
                Range ri = ranges.ElementAt(i);
                for (int j = i + 1; j < ranges.Count; ++j)
                {
                    Range rj = ranges.ElementAt(j);
                    if (ri.OverlapsWith(rj))
                    {
                        toRemove.Add(ri);
                        toRemove.Add(rj);
                        createdRanges.Add(ri.MergedWith(rj));
                    }
                }
            }

            ranges.ExceptWith(toRemove);
            ranges.UnionWith(createdRanges);
        } while (toRemove.Count != 0);

        return ranges.Select(r => r.Length).Sum().ToString();
    }
}