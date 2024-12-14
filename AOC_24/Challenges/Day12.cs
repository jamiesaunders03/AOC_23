using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;
using Math = System.Math;

namespace AOC_24.Challenges;

internal class Day12 : IAocChallenge
{
    private readonly struct Region
    {
        /// <summary>
        /// The identifier for the plant type
        /// </summary>
        public char Plant { get; init; }
        
        /// <summary>
        /// The plot positions that make up the region
        /// </summary>
        public ICollection<Vector2> Plots { get; init; }
        
        /// <summary>
        /// The perimeter of the region
        /// </summary>
        public int Perimeter { get; init; }
        
        /// <summary>
        /// The collection of plot tiles that lie on the edge of the map
        /// </summary>
        public ICollection<Vector2>[] Edges { get; init; }
        
        /// <summary>
        /// The area of the plot
        /// </summary>
        public int Area => Plots.Count;
    }
    
    public int Day => 12;

    private readonly List<Region> _regions;

    public Day12()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        _regions = GetAllRegions(input.ToGrid());
    }

    public string Challenge1()
    {
        return _regions
                    .Select(r => r.Area * r.Perimeter)
                    .Sum()
                    .ToString();
    }

    public string Challenge2()
    {
        Pair<char, int>[] sides = _regions
            .Select(r => new Pair<char, int>(r.Plant, GetNumberOfSides(r)))
            .ToArray();
        
        return _regions
            .Select(r => r.Area * GetNumberOfSides(r))
            .Sum()
            .ToString();
    }

    /// <summary>
    /// Gets the data about all regions within the given garden.
    /// The garden object will get mutated to mark all explored regions as `(char)0` when exploring each region.
    /// </summary>
    /// <param name="garden">The garden to explore.</param>
    /// <returns>The list of all regions in the garden</returns>
    private static List<Region> GetAllRegions(char[,] garden)
    {
        List<Region> regions = [];

        while (FindNewRegion(garden, out Vector2 pos))
        {
            regions.Add(SearchRegion(garden, pos));
        }
        
        return regions;
    }

    /// <summary>
    /// Finds a new region, if any exist. Sets `out pos` to the location of the new region if one is found.
    /// Returns true if a new region was found.
    /// </summary>
    /// <param name="garden">The garden to search</param>
    /// <param name="pos">The location of the found region.</param>
    /// <returns>Whether a new region was found.</returns>
    private static bool FindNewRegion(char[,] garden, out Vector2 pos)
    {
        pos = new Vector2();
        foreach (GridPointer<char> tile in Enumeration.EnumerateArray(garden))
        {
            if (tile.Value != (char)0)
            {
                pos = new Vector2(tile.Pos.Y, tile.Pos.X);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Given a start position, searches the region with the same plant to generate a region object.
    /// This mutates the garden array, replacing each element that gets assigned to this region with `(char)0`
    /// </summary>
    /// <param name="garden">The garden space to search through</param>
    /// <param name="startPos">The place to start creating this region from</param>
    /// <returns></returns>
    private static Region SearchRegion(char[,] garden, Vector2 startPos)
    {
        HashSet<Vector2> positions = [ ];
        HashSet<Vector2> toSearch = [ startPos ];
        ICollection<Vector2>[] edges = Utilities.Initialize<ICollection<Vector2>>(() => new HashSet<Vector2>(), 4);
        
        int perimeter = 0;
        char plant = Utilities.VectorIndex(garden, startPos);

        while (toSearch.Count != 0)
        {
            Vector2 pos = toSearch.First();
            toSearch.Remove(pos);

            positions.Add(pos);
            Utilities.SetVectorIndex(garden, pos, (char)0);
            
            foreach ((int i, Vector2 adjacent) in Enumeration.EnumerateArray(pos.Adjacent()))
            {
                if (Utilities.TryVectorIndex(garden, adjacent, out char c) && c == plant)
                {
                    toSearch.Add(adjacent);
                }
                else if (!positions.Contains(adjacent))
                {
                    edges[i].Add(adjacent);
                    perimeter++;
                }
            }
        }

        return new Region
        {
            Perimeter = perimeter,
            Plant = plant,
            Plots = positions,
            Edges = edges,
        };
    }
    
    /// <summary>
    /// Gets the total number of sides that make up a region
    /// </summary>
    /// <param name="r">The region to get the sides of</param>
    private static int GetNumberOfSides(Region r)
    {
        return r.Edges.Select(GetSides).Sum();
    }
    
    /// <summary>
    /// Gets the sides from 1 direction of a region
    /// </summary>
    /// <param name="edgesOnSide">The points that make up the edges from the given angle</param>
    /// <param name="dir">The direction that has been approached from (even = top/bottom, odd = left/right)</param>
    private static int GetSides(ICollection<Vector2> edgesOnSide, int dir)
    {
        int sides = 0;
        
        Func<Vector2, int> groupSelector = GetSelector(dir);
        Func<Vector2, int> compSelector = GetSelector(dir + 1);
        
        foreach (IGrouping<int, Vector2> levels in edgesOnSide.GroupBy(groupSelector))
        {
            ++sides;
            List<Vector2> sidesOnRow = levels.ToList();
            sidesOnRow.Sort((v1, v2) => compSelector(v1).CompareTo(compSelector(v2)));
            for (int i = 1; i < levels.Count(); ++i)
            {
                if (Math.Abs(compSelector(sidesOnRow[i]) - compSelector(sidesOnRow[i - 1])) != 1)
                {
                    ++sides;
                }
            }
        }

        return sides;
    }

    private static Func<Vector2, int> GetSelector(int prop)
    {
        return GetProp;

        int GetProp(Vector2 v)
        {
            return prop % 2 == 0 ? v.Y : v.X;
        }
    }
}