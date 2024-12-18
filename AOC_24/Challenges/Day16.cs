using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;
using Math = System.Math;

namespace AOC_24.Challenges;

internal class Day16 : IAocChallenge
{
    private struct Frame
    {
        public Vector2 Pos { get; init; }
        public Vector2 Dir { get; init; }
        public int Cost { get; init; }
        public List<Vector2> Path { get; init; }
    }
    
    public int Day => 16;

    private readonly bool[,] _maze;
    private readonly Frame[] _frames;

    public Day16()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        // string[] input = File.ReadAllLines("../../../TestCases/day16.txt");
        _maze = new bool[input.Length, input[0].Length];
        
        for (int y = 0; y < input.Length; ++y)
            for (int x = 0; x < input[0].Length; ++x)
                _maze[y, x] = input[y][x] != '#';
        
        _frames = GetBestFrames();
    }

    public string Challenge1()
    {
        return _frames[0].Cost.ToString();
    }

    public string Challenge2()
    {
        return _frames
            .SelectMany(f => f.Path)
            .ToHashSet()
            .Count
            .ToString();
    }

    private Frame[] GetBestFrames()
    {
        var start = new Vector2(1, _maze.GetLength(1) - 2);
        var end = new Vector2(_maze.GetLength(0) - 2, 1);
        List<Frame> bestFrames = [ new Frame { Cost = int.MaxValue} ];

        int[,] distMat = Utilities.Initialize(
            () => int.MaxValue, 
            _maze.GetLength(1), 
            _maze.GetLength(0));

        List<Frame> queue = [ new Frame
        {
            Pos = start, 
            Dir = Vector2.Right, 
            Cost = 0, 
            Path = [ start ],
        }];

        while (queue.Count != 0)
        {
            Frame f = queue[0];
            queue.RemoveAt(0);

            if (f.Pos == end)
            {
                UpdateBestFrames(ref bestFrames, f);
                continue;
            }

            if (f.Cost - 1000 > Utilities.VectorIndex(distMat, f.Pos))
                continue;

            foreach (Vector2 pos in f.Pos.Adjacent())
            {
                // Tile is in maze, is a path and is not a 180, and is not too long
                if (Utilities.TryVectorIndex(_maze, pos, out bool b) && b && f.Pos - pos != f.Dir)
                {
                    int cost = f.Cost + 1 + (pos - f.Pos != f.Dir ? 1000 : 0);
                    queue.Add(new Frame
                    {
                        Pos = pos, 
                        Dir = pos - f.Pos, 
                        Cost = cost,
                        Path = [ ..f.Path, pos ],
                    });
                }
            }

            if (f.Cost < Utilities.VectorIndex(distMat, f.Pos))
                Utilities.SetVectorIndex(distMat, f.Pos, f.Cost);
        }

        return bestFrames.ToArray();
    }

    private static void UpdateBestFrames(ref List<Frame> currentFrames, Frame newFrame)
    {
        if (newFrame.Cost < currentFrames[0].Cost)
            currentFrames.Clear();
        else if (newFrame.Cost <= currentFrames[0].Cost)
            currentFrames.Add(newFrame);
    }
}