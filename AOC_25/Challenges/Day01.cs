using System.Reflection;
using AocHelper;
using log4net;

namespace AOC_25.Challenges;

public class Day01 : IAocChallenge
{
    private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    
    public int Day => 1;

    private const int DIALS = 100;
    private const int START_POS = 50;
    private readonly List<int> _rotations;
    
    public Day01()
    {
        _logger.Info($"Constructing {GetType().Name} Solver");
        
        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split('\n');

        _rotations = new List<int>();
        foreach (string row in input)
        {
            int mult = row[0] == 'R' ? 1 : -1;
            int rest = int.Parse(new string(row.Skip(1).ToArray()));
            
            _rotations.Add(mult * rest);
        }
    }
    
    /// <summary>
    /// Count the number of times the counter ends on zero after a rotation
    /// </summary>
    public string Challenge1()
    {
        _logger.Info($"Starting {GetType().Name} Part 1");

        int count = 0;
        int currentPos = START_POS;

        foreach (int value in _rotations)
        {
            currentPos = (currentPos + value) % DIALS;
            if (currentPos == 0)
                ++count;
        }

        return count.ToString();
    }

    /// <summary>
    /// Count the number of times the counter either ends on, or passes zero, during a rotation
    /// </summary>
    public string Challenge2()
    {
        _logger.Info($"Starting {GetType().Name} Part 2");
        
        int count = 0;
        int currentPos = START_POS;

        foreach (int value in _rotations)
        {
            if (value > 0)
            {
                for (int i = 0; i < value; ++i)
                {
                    currentPos += 1;
                    if (currentPos % 100 == 0)
                        ++count;
                }
            }
            else
            {
                for (int i = 0; i < -value; ++i)
                {
                    currentPos -= 1;
                    if (currentPos % 100 == 0)
                        ++count;
                }
            }
        }
        
        return count.ToString();  // 6358
    }
}