using System.Text.RegularExpressions;

using AocHelper;
using AocHelper.Algorithms;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day18 : IAocChallenge
{
    private const int SIZE = 71;
    private const int BYTES_TO_FALL = 1024;
    
    public int Day => 18;

    private readonly List<Vector2> _bytes;

    public Day18()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');

        _bytes = [];
        foreach (string line in input)
        {
            Match m = RegexPaterns.VectorRegex.Match(line);
            _bytes.Add(Vector2.FromMatch(m));
        }
    }

    public string Challenge1()
    {
        int[,] map = Utilities.Initialize(int.MaxValue, SIZE, SIZE);
        for (int i = 0; i < BYTES_TO_FALL; ++i)
        {
            Vector2 pos = _bytes[i];
            Utilities.SetVectorIndex(map, pos, int.MinValue);
        }

        Vector2 start = new(0, 0);
        Vector2 end = new(SIZE - 1, SIZE - 1);
        ShortestPath(map, start, end);

        return Utilities.VectorIndex(map, end).ToString();
    }

    public string Challenge2()
    {
        int drops = BYTES_TO_FALL;
        Vector2 start = new(0, 0);
        Vector2 end = new(SIZE - 1, SIZE - 1);

        HashSet<Vector2> shortestPath = _bytes.ToHashSet();

        while (true)
        {
            if (!shortestPath.Contains(_bytes[drops]))
            {
                ++drops;
                continue;
            }
            
            int[,] map = Utilities.Initialize(int.MaxValue, SIZE, SIZE);
            for (int i = 0; i < drops; ++i)
            {
                Vector2 pos = _bytes[i];
                Utilities.SetVectorIndex(map, pos, int.MinValue);
            }
            
            ShortestPath(map, start, end);
            if (Utilities.VectorIndex(map, end) == int.MaxValue)
                break;

            ++drops;
            shortestPath = PathFinding.GetShortestPathFromMaze(map, start, end).ToHashSet();
        }

        Vector2 blocker = _bytes[drops - 1];
        return $"{blocker.X},{blocker.Y}";
    }

    /// <summary>
    /// Computes the shortest path through the maze. It treats int.MinValue tiles as in-accessible.
    /// The path object is mutated and the value of each position is the shortest distance to get to that position.
    /// </summary>
    /// <param name="map">The map to search</param>
    /// <param name="start">The start position of the maze</param>
    /// <param name="end">The end position of the maze</param>
    private void ShortestPath(int[,] map, Vector2 start, Vector2 end)
    {
        Queue<Vector2> queue = new();
        queue.Enqueue(start);
        Utilities.SetVectorIndex(map, start, 0);
        
        while (queue.Count != 0)
        {
            Vector2 pos = queue.Dequeue();
            if (pos == end) 
                continue;

            int currentPosVal = Utilities.VectorIndex(map, pos);
            foreach (Vector2 adj in pos.Adjacent())
            {
                if (Utilities.TryVectorIndex(map, adj, out int val))
                {
                    if (val != int.MinValue && currentPosVal + 1 < val)
                    {
                        Utilities.SetVectorIndex(map, adj, currentPosVal + 1);
                        queue.Enqueue(adj);
                    }
                }
            }
        }
    }
}