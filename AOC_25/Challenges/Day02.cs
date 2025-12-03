using AocHelper;
using log4net;
using Range = AocHelper.DataStructures.Range;

namespace AOC_25.Challenges;

public class Day02 : IAocChallenge
{
    #region Boilerplate

    private static readonly ILog _logger = LogManager.GetLogger(typeof(Day02));
    
    public int Day => 2;

    #endregion

    private readonly List<Range> _ranges;

    public Day02()
    {
        _logger.Info($"Constructing {nameof(Day01)} Solver");
        
        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split(',');
        _ranges = input.Select(s => {
                            string[] parts = s.Split('-');
                            long firstNum = long.Parse(parts[0]);
                            return new Range(firstNum, long.Parse(parts[1]) - firstNum);
                        }).ToList();
    }
    
    /// <summary>
    /// Sum the numbers in the given ranges that are 2 repeated numbers
    /// E.g. 6464 is '64' twice, 11 is `1' twice
    /// Numbers like 777 are not included
    /// </summary>
    public string Challenge1()
    {
        _logger.Info($"Starting {nameof(Day02)} Part 1");

        long total = 0;
        foreach (Range r in _ranges)
        {
            string str = r.Start.ToString();
            string firstHalf = new(str.Take(str.Length / 2).ToArray());
            
            if (firstHalf == "")
                firstHalf = "1";
            
            while (true)
            {
                long number = long.Parse(firstHalf + firstHalf);
                if (r.Contains(number))
                    total += number;
                else if (number > r.Start + r.Length)
                    break;
                
                firstHalf = (long.Parse(firstHalf) + 1).ToString();
            }
        }

        return total.ToString();
    }

    /// <summary>
    /// Part 1 but the pattern can repeat multiple times, so '171717' would now be valid
    /// </summary>
    public string Challenge2()
    {
        _logger.Info($"Starting {nameof(Day02)} Part 2");
        
        long total = 0;
        foreach (Range r in _ranges)
        {
            string str = r.Start.ToString();
            for (int div = 2; div < str.Length; ++div)
            {
                string firstPart = new(str.Take(str.Length / div).ToArray());

                if (firstPart == "")
                    firstPart = "0";

                while (true)
                {
                    long number = long.Parse(string.Join("", Enumerable.Repeat(firstPart, div)));
                    if (r.Contains(number))
                        total += number;
                    else if (number > r.Start + r.Length)
                        break;

                    firstPart = (long.Parse(firstPart) + 1).ToString();
                }
            }
        }

        return total.ToString();  // 27324587484 too high
    }
}