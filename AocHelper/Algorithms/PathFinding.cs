using System.Collections;
using AocHelper.DataStructures;

namespace AocHelper.Algorithms;

public static class PathFinding
{
    /// <summary>
    /// Given a maze of adjacency values, computes the shortest path from start to finish for the maze.
    /// A step of 1 between positions is assumed. The returned collection represents each position of the route
    /// in order from start to finish.
    /// </summary>
    /// <param name="map">The completed map to search</param>
    /// <param name="start">The start position of the maze</param>
    /// <param name="end">The end position of the maze</param>
    /// <returns></returns>
    public static ICollection<Vector2> GetShortestPathFromMaze(int[,] map, Vector2 start, Vector2 end)
    {
        List<Vector2> path = [end];

        while (path.Last() != start)
        {
            int current = Utilities.Utilities.VectorIndex(map, path.Last());
            foreach (Vector2 adj in path.Last().Adjacent())
            {
                if (Utilities.Utilities.TryVectorIndex(map, adj, out int val) && val == current - 1)
                {
                    path.Add(adj);
                    break;
                }
            }
        }

        path.Reverse();
        return path;
    }
}