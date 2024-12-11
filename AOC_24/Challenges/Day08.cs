using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day08 : IAocChallenge
{
    public int Day => 8;

    private readonly DefaultDictionary<char, List<Vector2>> _antennae;
    private readonly int _width;
    private readonly int _height;

    public Day08()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        // string[] input = File.ReadAllLines("../../../TestCases/day8b.txt");
        _antennae = new DefaultDictionary<char, List<Vector2>>();
        _height = input.Length;
        _width = input[0].Length;

        foreach (GridPointer<char> pointer in Enumeration.EnumerateArray(input))
        {
            if (pointer.Value != '.')
                _antennae[pointer.Value].Add(pointer.Pos);
        }
    }

    public string Challenge1()
    {
        var points = new HashSet<Vector2>();
        
        foreach (List<Vector2> antennae in _antennae.Values)
        {
            points.UnionWith(GetAdjacentAntiNodesForSatellites(antennae));
        }

        return points.Count.ToString();
    }

    public string Challenge2()
    {
        var points = new HashSet<Vector2>();
        
        foreach (List<Vector2> antennae in _antennae.Values)
        {
            points.UnionWith(GetAllAntiNodesForSatellites(antennae));
        }

        return points.Count.ToString();
    }
    
    private HashSet<Vector2> GetAdjacentAntiNodesForSatellites(List<Vector2> satellites)
    {
        if (satellites.Count == 1)
            return [];
        
        var points = new HashSet<Vector2>();
        foreach (List<Vector2> pair in Enumeration.Combinations(satellites, 2))
        {
            Vector2 delta = pair[1] - pair[0];

            points.Add(pair[0] - delta);
            points.Add(pair[1] + delta);
        }

        points = points
            .Where(p => p is { X: >= 0, Y: >= 0 } && p.X < _width && p.Y < _height)
            .ToHashSet();
        
        return points;
    }
    
    private HashSet<Vector2> GetAllAntiNodesForSatellites(List<Vector2> satellites)
    {
        if (satellites.Count == 1)
            return [];
        
        var points = new HashSet<Vector2>();
        foreach (List<Vector2> pair in Enumeration.Combinations(satellites, 2))
        {
            Vector2 delta = pair[1] - pair[0];
            
            points.Add(pair[0]);
            
            Vector2 point = new Vector2(pair[0]) + delta;
            while (point is { X: >= 0, Y: >= 0 } && point.X < _width && point.Y < _height)
            {
                points.Add(point);
                point += delta;
            }
            
            point = new Vector2(pair[0]) - delta;
            while (point is { X: >= 0, Y: >= 0 } && point.X < _width && point.Y < _height)
            {
                points.Add(point);
                point -= delta;
            }
        }
        
        return points;
    }
}