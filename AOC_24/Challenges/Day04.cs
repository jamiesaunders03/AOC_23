
using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day04 : IAocChallenge
{
    public int Day => 4;

    private const string TARGET_STRING = "XMAS";
    private const string X_MAS_LOOP_COMBINATIONS = "MSSMMSS";
    private static readonly Vector2[] _corners = [
        new Vector2(1, 1), 
        new Vector2(-1, 1), 
        new Vector2(-1, -1), 
        new Vector2(1, -1),
    ];
    
    private readonly char[,] _searchGrid;

    public Day04()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        // string[] input = File.ReadAllLines("../../../TestCases/day4.txt");
        _searchGrid = input.ToGrid();
    }

    public string Challenge1()
    {
        Vector2[] dirs = Vector2.Directions;

        int matches = Enumeration
            .EnumerateArray(_searchGrid)
            .Select(pointer => dirs.Count(dir => IsXMas(pointer.Pos, dir)))
            .Sum();

        return matches.ToString();
    }

    public string Challenge2()
    {
        int matches = 0;
        
        foreach (GridPointer<char> pointer in Enumeration.EnumerateArray(_searchGrid))
        {
            Vector2 pos = pointer.Pos;
            if (Utilities.VectorIndex(_searchGrid, pos) != 'A')
                continue;

            List<char> corners = GetCornerChars(pos);
            if (X_MAS_LOOP_COMBINATIONS.Contains(string.Join("", corners)))
                ++matches;
            
        }

        return matches.ToString();
    }

    private bool IsXMas(Vector2 v, Vector2 dir)
    {
        var pos = new Vector2(v);
                
        foreach (char current in TARGET_STRING)
        {
            if (!Utilities.TryVectorIndex(_searchGrid, pos, out char c) || c != current)
                return false;
                    
            pos += dir;
        }

        return true;
    }
    
    private List<char> GetCornerChars(Vector2 pos)
    {
        // Check diagonal corners
        var corners = new List<char>();
        foreach (Vector2 diff in _corners)
        {
            Utilities.TryVectorIndex(_searchGrid, pos + diff, out char corner);
            corners.Add(corner);
        }

        return corners;
    }
}