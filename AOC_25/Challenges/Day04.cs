using AocHelper;
using log4net;

namespace AOC_25.Challenges;

public class Day04 : IAocChallenge
{
    #region Boilerplate

    private static readonly ILog _logger = LogManager.GetLogger(typeof(Day04));

    public int Day => 4;

    #endregion
    
    public Day04()
    {
        _logger.Info($"Constructing {GetType().Name} Solver");

        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split('\n');
        // todo
    }
    
    public string Challenge1()
    {
        _logger.Info($"Starting {GetType().Name} Part 1");
        
        throw new NotImplementedException();
    }

    public string Challenge2()
    {
        _logger.Info($"Starting {GetType().Name} Part 2");
        
        throw new NotImplementedException();
    }
}