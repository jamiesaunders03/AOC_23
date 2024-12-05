using AocHelper;

namespace AOC_24.Challenges;

internal class Day05 : IAocChallenge
{
    public int Day => 5;

    private readonly List<(int, int)> _rules;
    private readonly List<int[]> _pages;

    public Day05()
    {
        // First item is rules, second is pages
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split("\n\n");

        _rules = new List<(int, int)>();
        _pages = new List<int[]>();
        
        foreach (string line in input[0].Split('\n'))
        {
            string[] parts = line.Split('|');
            _rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }

        foreach (string line in input[1].Split('\n'))
        {
            string[] parts = line.Split(',');
            _pages.Add(parts.Select(int.Parse).ToArray());
        }
    }

    public string Challenge1()
    {
        int total = 0;
        
        foreach (int[] page in _pages)
        {
            bool accepted = true;
            for (int i = 0; i < page.Length; ++i)
            {
                foreach (int num in page.Skip(i + 1))
                {
                    if (_rules.Contains((num, page[i])))
                    {
                        accepted = false;
                        break;
                    }
                }
            }

            if (accepted)
                ++total;
        }

        return total.ToString();
    }

    public string Challenge2()
    {
        throw new NotImplementedException();
    }
}