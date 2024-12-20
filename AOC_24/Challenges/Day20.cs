using System.Collections;
using AocHelper;
using AocHelper.Algorithms;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day20 : IAocChallenge
{
    private const int SKIP_AMOUNT = 100;
    private const int CHEAT_DIST_PART_1 = 2;
    private const int CHEAT_DIST_PART_2 = 20;
    
    public int Day => 20;

    private readonly bool[,] _maze;
    private readonly Vector2 _startPos;
    private readonly Vector2 _endPos;

    public Day20()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        _maze = new bool[input.Length, input[0].Length];
        
        foreach (GridPointer<char> gridPtr in Enumeration.EnumerateArray(input))
        {
            var indexVector = new Vector2(gridPtr.Pos.Y, gridPtr.Pos.X);
            Utilities.SetVectorIndex(_maze, indexVector, gridPtr.Value != '#');

            if (gridPtr.Value == 'S')
                _startPos = indexVector;
            else if (gridPtr.Value == 'E')
                _endPos = indexVector;
        }
    }

    public string Challenge1()
    {
        int[,] grid = PathFinding.PathFind(_maze, _startPos, _endPos);
        ICollection<Vector2> path = PathFinding.GetShortestPathFromMaze(grid, _startPos, _endPos);

        int bigSkips = GetBigSkips(grid, path, CHEAT_DIST_PART_1);
        return bigSkips.ToString();
    }

    public string Challenge2()
    {
        int[,] grid = PathFinding.PathFind(_maze, _startPos, _endPos);
        ICollection<Vector2> path = PathFinding.GetShortestPathFromMaze(grid, _startPos, _endPos);

        int bigSkips = GetBigSkips(grid, path, CHEAT_DIST_PART_2);
        return bigSkips.ToString();
    }

    private static int GetBigSkips(int[,] grid, ICollection<Vector2> path, int cheatDist)
    {
        int bigSkips = 0;
        foreach (Vector2 point in path)
        {
            int cost = Utilities.VectorIndex(grid, point);
            foreach (Vector2 near in point.Nearby(cheatDist))
            {
                long skipDist = point.Manhattan(near);
                if (Utilities.TryVectorIndex(grid, near, out int skippedCost) 
                    && skippedCost != int.MaxValue
                    && skippedCost - cost - skipDist >= SKIP_AMOUNT)
                    ++bigSkips;
            }
        }

        return bigSkips;
    }
}