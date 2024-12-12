using AocHelper;
using AocHelper.DataStructures;

namespace AOC_24.Challenges;

internal class Day05 : IAocChallenge
{
    public int Day => 5;

    private readonly HashSet<Pair<int, int>> _rules;
    private readonly List<int[]> _pages;

    public Day05()
    {
        // First item is rules, second is pages
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split("\n\n");

        _rules = new HashSet<Pair<int, int>>();
        _pages = new List<int[]>();
        
        foreach (string line in input[0].Split('\n'))
        {
            string[] parts = line.Split('|');
            _rules.Add(new Pair<int, int>(int.Parse(parts[0]), int.Parse(parts[1])));
        }

        foreach (string line in input[1].Split('\n'))
        {
            string[] parts = line.Split(',');
            _pages.Add(parts.Select(int.Parse).ToArray());
        }
    }

    public string Challenge1()
    {
        int total = _pages.Where(AcceptPages).Sum(MiddlePageValue);
        return total.ToString();
    }

    public string Challenge2()
    {
        List<int[]> rejected = _pages.Where(page => !AcceptPages(page)).ToList();
        
        int total = rejected
            .Select(SortPages)
            .Sum(MiddlePageValue);
        
        return total.ToString();
    }

    private bool AcceptPages(int[] page)
    {
        for (int i = 0; i < page.Length; ++i)
        {
            if (page
                .Skip(i + 1)
                .Any(num => _rules.Contains(new Pair<int, int>(num, page[i]))))
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Create a new array of the pages, sorted by the rules in the puzzle.
    /// </summary>
    /// <param name="page">The page to sort</param>
    /// <returns></returns>
    private int[] SortPages(int[] page)
    {
        List<int> pages = page.ToList();
        int len = pages.Count;

        int i = 0;
        while (i < len)
        {
            bool changes = false;
            int current = pages[i];
            
            // Check all numbers ahead of current, and move before if they violate a rule,
            // maintaining order of moved pages
            for (int j = i + 1; j < len; ++j)
            {
                int target = pages[j];
                if (_rules.Contains(new Pair<int, int>(target, current)))
                {
                    // Move j to before current
                    pages.RemoveAt(j);
                    pages.Insert(i, target);
                    changes = true;
                }
            }

            if (!changes)
                ++i;
        }

        return pages.ToArray();
    }

    private static int MiddlePageValue(int[] pages)
    {
        return pages[pages.Length / 2];
    }
}