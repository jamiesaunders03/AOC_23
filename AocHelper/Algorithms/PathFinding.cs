using AocHelper.DataStructures;

namespace AocHelper.Algorithms;

public static class PathFinding
{
    /// <summary>
    /// Given a 2D array representing the available paths (true is a path, false is not) returns an array representing
    /// the distance to each point from the start position, stopping at the end position.
    /// </summary>
    /// <param name="maze">The paths that can be taken through the maze</param>
    /// <param name="start">The start position to search from</param>
    /// <param name="end">The target position to find the distance to</param>
    /// <returns></returns>
    public static int[,] PathFind(bool[,] maze, Vector2 start, Vector2 end)
    {
        int[,] map = Utilities.Utilities.Initialize(int.MaxValue, maze.GetLength(0), maze.GetLength(1));
        
        Queue<Vector2> queue = new();
        queue.Enqueue(start);
        Utilities.Utilities.SetVectorIndex(map, start, 0);
        
        while (queue.Count != 0)
        {
            Vector2 pos = queue.Dequeue();
            if (pos == end) 
                continue;
            
            int currentPosVal = Utilities.Utilities.VectorIndex(map, pos);
            foreach (Vector2 adj in pos.Adjacent())
            {
                // if valid next path
                if (Utilities.Utilities.TryVectorIndex(maze, adj, out bool valid) && valid)
                {
                    // if moving would improve score
                    if (currentPosVal + 1 < Utilities.Utilities.VectorIndex(map, adj))
                    {
                        Utilities.Utilities.SetVectorIndex(map, adj, currentPosVal + 1);
                        queue.Enqueue(adj);
                    }
                }
            }
        }

        return map;
    }
    
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