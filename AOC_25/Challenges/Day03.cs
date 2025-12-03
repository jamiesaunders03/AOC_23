using AocHelper;
using log4net;

namespace AOC_25.Challenges;

public class Day03 : IAocChallenge
{
    #region Boilerplate

    private static readonly ILog _logger = LogManager.GetLogger(typeof(Day02));

    public int Day => 3;

    #endregion

    private readonly List<List<byte>> _batteryArray = [];

    public Day03()
    {
        _logger.Info($"Constructing {nameof(Day01)} Solver");

        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split('\n');
        foreach (string row in input)
        {
            List<byte> jolts = row.Select(c => (byte)(c - '0')).ToList();
            _batteryArray.Add(jolts);
        }
    }

    /// <summary>
    /// Get the highest number from 2 chars in each row. They can be in any position but order must be preserved.
    /// E.g. 514925 would be 95
    ///      123456 would be 56
    ///      636549 would be 69 
    /// </summary>
    public string Challenge1()
    {
        long totalJolts = 0;
        foreach (List<byte> joltBattery in _batteryArray)
            totalJolts += GetTotalJolts(joltBattery, 2);

        return totalJolts.ToString();
    }

    /// <summary>
    /// Challenge 1, but with exactly 12 batteries turned on, instead of just 2.
    /// </summary>
    public string Challenge2()
    {
        long totalJolts = 0;
        foreach (List<byte> joltBattery in _batteryArray)
            totalJolts += GetTotalJolts(joltBattery, 12);

        return totalJolts.ToString();
    }

    private long GetTotalJolts(List<byte> battery, int switchesAvailable)
    {
        List<int> row = [];
        int index = 0;

        while (switchesAvailable != 0)
        {
            int max = 0;
            for (int i = index; i <= battery.Count - switchesAvailable; ++i)
            {
                if (battery[i] > max)
                {
                    max = battery[i];
                    index = i + 1;
                }

                if (max == 9)
                    break;
            }

            row.Add(max);
            --switchesAvailable;
        }
        
        long total = 0;
        foreach (int item in row)
        {
            total *= 10;
            total += item;
        }
        
        return total;
    }
}