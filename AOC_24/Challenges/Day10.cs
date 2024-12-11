using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day10 : IAocChallenge
{
    public int Day => 10;

    private readonly char[,] _map;

    public Day10()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        _map = input.ToGrid();
    }

    public string Challenge1()
    {
        Vector2[] startPoints = Enumeration.EnumerateArray(_map)
            .Where(pointer => pointer.Value == '0')
            .Select(pointer => pointer.Pos)
            .ToArray();

        int routes = startPoints
            .Select(GetAllRoutes)
            .Select(r => new HashSet<Vector2>(r).Count)
            .Sum();

        return routes.ToString();
    }

    public string Challenge2()
    {
        Vector2[] startPoints = Enumeration.EnumerateArray(_map)
            .Where(pointer => pointer.Value == '0')
            .Select(pointer => pointer.Pos)
            .ToArray();

        int routes = startPoints
            .Select(GetAllRoutes)
            .Select(r => r.Count)
            .Sum();

        return routes.ToString();
    }

    /// <summary>
    /// Gets the total number of routes for the given starting point
    /// </summary>
    /// <param name="startPos">The starting position on the map</param>
    /// <returns>The total number of valid routes</returns>
    private List<Vector2> GetAllRoutes(Vector2 startPos)
    {
        List<Vector2> currentFrame = [ startPos ];

        for (int h = 1; h <= 9; ++h)
        {
            List<Vector2> nextFrame = [];
            foreach (Vector2 neighbor in currentFrame.SelectMany(v => v.Adjacent()))
            {
                if (Utilities.TryVectorIndex(_map, new Vector2(neighbor.Y, neighbor.X), out char c) && c - '0' == h)
                {
                    nextFrame.Add(neighbor);
                }
            }

            currentFrame = nextFrame;
        }
        
        return currentFrame;
    }
}