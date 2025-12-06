using AocHelper;
using AocHelper.Utilities;
using NLog;

namespace AOC_25.Challenges;

public class Day06 : IAocChallenge
{
    #region Boilerplate

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public int Day => 06;

    #endregion
    
    private readonly List<List<string>> _nums = [];
    private readonly List<char> _ops;

    public Day06()
    {
        _logger.Info($"Constructing {GetType().Name} Solver");

        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split('\n');
        List<int> segments = GetStartPositions(input);

        for (int i = 0; i < segments.Count - 1; ++i)
        {
            List<string> colParts = [];
            foreach (string row in input.SkipLast(1))
                colParts.Add(row.Substring(segments[i], segments[i + 1] - segments[i] - 1));
            
            _nums.Add(colParts);
        }

        _ops = input.Last().Where(c => c != ' ').ToList();
    }

    /// <summary>
    /// Math, but vertical
    /// </summary>
    public string Challenge1()
    {
        _logger.Info($"Starting {GetType().Name} Part 1");

        List<List<long>> nums = [];
        foreach (List<string> numCol in _nums)
        {
            List<long> colNums = numCol.Select(n => long.Parse(n.Trim())).ToList();
            nums.Add(colNums);
        }
        
        return Math(nums).ToString();
    }

    /// <summary>
    /// Math, but the numbers are vertical
    /// </summary>
    public string Challenge2()
    {
        _logger.Info($"Starting {GetType().Name} Part 2");

        List<List<long>> newNums = [];

        for (int i = 0; i < _ops.Count; ++i)
            newNums.Add(GetNewNums(_nums[i]));

        return Math(newNums).ToString();
    }
    
    #region Parsing

    /// <summary>
    /// Includes start and end for indices use
    /// </summary>
    private static List<int> GetStartPositions(ICollection<string> rows)
    {
        List<int> empty = [0];

        for (int i = 1; i < rows.ElementAt(0).Length; ++i)
        {
            if (rows.All(r => r[i] == ' '))
                empty.Add(i + 1);
        }
        
        empty.Add(rows.ElementAt(0).Length + 1);
        return empty;
    }
    
    #endregion

    /// <summary>
    /// Given a list of each column set of number, computes the sum/product of each column
    /// depending on the value in <see cref="_ops"/>
    /// </summary>
    private long Math(List<List<long>> nums)
    {
        long total = 0;
        for (int i = 0; i < _ops.Count; ++i)
        {
            long outcome;
            if (_ops[i] == '+')
                outcome = nums[i].Sum();
            else
                outcome = nums[i].Prod();
            
            total += outcome;
        }

        return total;
    }

    /// <summary>
    /// Gets the numbers down each column of the 'numbers' list
    /// </summary>
    private static List<long> GetNewNums(List<string> nums)
    {
        List<long> newNums = [];

        for (int i = 0; i < nums[0].Length; ++i)
        {
            char[] chars = nums.Select(r => r[i]).Where(c => c != ' ').ToArray();
            newNums.Add(chars.InterpretAsLong());
        }

        return newNums;
    }
}