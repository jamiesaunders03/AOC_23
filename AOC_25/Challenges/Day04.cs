using AocHelper;
using NLog;

namespace AOC_25.Challenges;

public class Day04 : IAocChallenge
{
    #region Boilerplate

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public int Day => 4;

    #endregion

    private readonly List<List<bool>> _grid = [];
    
    public Day04()
    {
        _logger.Info($"Constructing {GetType().Name} Solver");

        string[] input = new FetchData(Day, 2025).ReadInput().TrimEnd().Split('\n');
        foreach (string row in input)
        {
            List<bool> rolls = row.Select(c => c == '@').ToList();
            _grid.Add(rolls);
        }
    }
    
    /// <summary>
    /// Convolve, get places less than 4 neighbors
    /// </summary>
    public string Challenge1()
    {
        _logger.Info($"Starting {GetType().Name} Part 1");

        long total = 0;

        for (int x = 0; x < _grid[0].Count; ++x)
            for (int y = 0; y < _grid.Count; ++y)
                if (_grid[y][x] && ConvolvePosition(x, y) < 4)
                    ++total;

        return total.ToString();
    }

    /// <summary>
    /// Part 1, remove items affected until no more can be done
    /// </summary>
    public string Challenge2()
    {
        _logger.Info($"Starting {GetType().Name} Part 2");
        
        long total = 0;
        while (true)
        {
            List<(int, int)> positions = [];
            for (int x = 0; x < _grid[0].Count; ++x)
                for (int y = 0; y < _grid.Count; ++y)
                    if (_grid[y][x] && ConvolvePosition(x, y) < 4)
                        positions.Add((x, y));

            if (positions.Count == 0)
                break;

            total += positions.Count;
            foreach ((int x, int y) position in positions)
                _grid[position.y][position.x] = false;
        }

        return total.ToString();
    }

    private int ConvolvePosition(int x, int y)
    {
        List<(int, int)> positions =
        [
            (x - 1, y - 1),
            (x, y - 1),
            (x + 1, y - 1),
            (x - 1, y),
            (x + 1, y),
            (x - 1, y + 1),
            (x, y + 1),
            (x + 1, y + 1),
        ];

        int count = 0;
        foreach ((int x, int y) pos in positions)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= _grid[0].Count || pos.y >= _grid.Count)
                continue;
            
            if (_grid[pos.y][pos.x])
                ++count;
        }

        return count;
    }
}