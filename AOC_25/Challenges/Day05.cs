using System.Reflection;

using AocHelper;
using log4net;

namespace AOC_25.Challenges;

public class Day05 : IAocChallenge
{
    #region Boilerplate

    private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public int Day => 04;

    #endregion

    public Day05()
    {
        _logger.Info($"Constructing {GetType().Name} Solver");

        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split('\n');
    }

    /// <summary>
    /// todo
    /// </summary>
    public string Challenge1()
    {
        _logger.Info($"Starting {GetType().Name} Part 1");

        throw new NotImplementedException();
    }

    /// <summary>
    /// todo
    /// </summary>
    public string Challenge2()
    {
        _logger.Info($"Starting {GetType().Name} Part 2");

        throw new NotImplementedException();
    }
}