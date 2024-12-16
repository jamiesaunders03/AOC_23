using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day06 : IAocChallenge
{
    private const char START = '^';
    private const char OBSTACLE = '#';
    
    public int Day => 6;

    private readonly char[,] _map;
    private Vector2? _startPositon = null;

    public Day06()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        _map = input.ToGrid();
    }

    public string Challenge1()
    {
        return GetAllLocationsOnRoute().Count.ToString();
    }

    public string Challenge2()
    {
        HashSet<Vector2> locs = GetAllLocationsOnRoute();
        locs.Remove(GetStartLocation());

        int infLoops = locs.Count(CausesInfLoop);
        return infLoops.ToString();
    }
    
    private HashSet<Vector2> GetAllLocationsOnRoute() {
        Vector2 current = GetStartLocation();
        Vector2 direction = Vector2.Down;
        HashSet<Vector2> visited = [ current ];

        while (Utilities.TryVectorIndex(_map, current + direction, out char c))
        {
            if (c == OBSTACLE)
            {
                direction = new Vector2(-direction.Y, direction.X);
            }
            else
            {
                current += direction;
                visited.Add(current);
            }
        }

        return visited;
    }

    private bool CausesInfLoop(Vector2 pos)
    {
        _map[pos.Y, pos.X] = '#';
        bool causesLoop = false;
        
        Vector2 current = GetStartLocation();
        Vector2 direction = Vector2.Down;
        HashSet<int> visitedWithDir = [];

        while (Utilities.TryVectorIndex(_map, current + direction, out char c) && !causesLoop)
        {
            if (c == OBSTACLE)
            {
                direction = new Vector2(-direction.Y, direction.X);
            }
            else
            {
                current += direction;
            }

            int visitDir = GetPosDirScore(current, direction);
            if (!visitedWithDir.Add(visitDir))
            {
                causesLoop = true;
            }
        }
        
        _map[pos.Y, pos.X] = '.';
        return causesLoop;
    }

    private static int GetPosDirScore(Vector2 pos, Vector2 dir)
    {
        int score = (int)(pos.X + 1_000 * pos.Y);
        score += (int)(1_000_000 * dir.X + 2_000_000 * dir.Y);

        return score;
    }

    private Vector2 GetStartLocation()
    {
        if (_startPositon is not null)
        {
            return _startPositon;
        }

        foreach (GridPointer<char> pointer in Enumeration.EnumerateArray(_map))
        {
            if (pointer.Value == START)
            {
                _startPositon = new Vector2(pointer.Pos.Y, pointer.Pos.X);
                return _startPositon;
            }
        }

        throw new Exception("Could not find start location");
    }
}