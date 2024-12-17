using System.Text.RegularExpressions;

using AocHelper;
using AocHelper.DataStructures;

namespace AOC_24.Challenges;

internal partial class Day14 : IAocChallenge
{
    private class Robot
    {
        public Vector2 Pos { get; set; }
        public Vector2 Velocity { get; init; }
        
        public override string ToString()
        {
            return $"Robot({Pos})";
        }

        public static Robot Parse(string line)
        {
            Match m = _robotRe.Match(line);
            return new Robot
            {
                Pos = new Vector2(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)), 
                Velocity = new Vector2(int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value)), 
            };
        }
    }

    private const int WIDTH = 101;
    private const int HEIGHT = 103;
    private const int PART1_ITERS = 100;
    
    [GeneratedRegex(@"p=([-\d]+),([-\d]+) v=([-\d]+),([-\d]+)")]
    private static partial Regex RobotRegex();
    private static readonly Regex _robotRe = RobotRegex();
    
    public int Day => 14;

    private readonly Robot[] _robots;

    public Day14()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        _robots = input.Select(Robot.Parse).ToArray();
    }

    public string Challenge1()
    {
        var robots = (Robot[])_robots.Clone();
        for (int i = 0; i < PART1_ITERS; ++i)
        {
            IterateMovements(robots);
        }

        int[] quadrants = new int[5];
        foreach (Robot r in robots)
        {
            int quad = GetQuadrant(r.Pos);
            quadrants[quad] += 1;
        }
        
        return (quadrants[1] * quadrants[2] * quadrants[3] * quadrants[4]).ToString();
    }

    public string Challenge2()
    {
        var robots = (Robot[])_robots.Clone();
        int i = PART1_ITERS;
        while (true)
        {
            IterateMovements(robots);
            ++i;
            
            if (HasTree())
                break;
        }

        return i.ToString();
    }

    private static void IterateMovements(Robot[] robots)
    {
        for (int i = 0; i < robots.Length; ++i)
        {
            Robot r = robots[i];
            robots[i].Pos = new Vector2(
                    (r.Pos.X + r.Velocity.X + WIDTH) % WIDTH, 
                    (r.Pos.Y + r.Velocity.Y + HEIGHT) % HEIGHT);
        }
    }

    private static int GetQuadrant(Vector2 v)
    {
        int horQuad = 0;
        int verQuad = 0;

        if (v.X < WIDTH / 2)
            horQuad = 1;
        else if (v.X > WIDTH / 2)
            horQuad = 2;
        
        if (v.Y < HEIGHT / 2)
            verQuad = 1;
        else if (v.Y > HEIGHT / 2)
            verQuad = 3;

        if (horQuad == 0 || verQuad == 0)
        {
            return 0;
        }
     
        return horQuad + verQuad - 1;
    }

    private bool HasTree()
    {
        const int SEARCH_LEN = 7;
        
        HashSet<Vector2> pos = _robots
            .Select(r => r.Pos)
            .ToHashSet();
        
        for (int y = 0; y < HEIGHT; ++y)
        {
            for (int x = 0; x < WIDTH - SEARCH_LEN; ++x)
            {
                bool hasRun = true;
                for (int i = 0; i < SEARCH_LEN; ++i)
                {
                    if (!pos.Contains(new Vector2(x + i, y)))
                    {
                        hasRun = false;
                        break;
                    }
                }

                if (hasRun)
                    return true;
            }
        }

        return false;
    }
}