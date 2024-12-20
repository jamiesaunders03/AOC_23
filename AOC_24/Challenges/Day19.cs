using AocHelper;

namespace AOC_24.Challenges;

internal class Day19 : IAocChallenge
{
    public int Day => 19;

    private readonly List<string> _cloths;
    private readonly string[] _targets;

    public Day19()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        
        _cloths = input[0].Split(", ").ToList();
        _cloths.Sort((first, second) => first.Length.CompareTo(second.Length));
        _cloths.Reverse();
        
        _targets = input.Skip(2).ToArray();
    }

    public string Challenge1()
    {
        int total = _targets.Count(t => TowelCombinations(t) != 0);
        return total.ToString();
    }

    public string Challenge2()
    {
        long total = _targets.Sum(TowelCombinations);
        return total.ToString();;
    }

    private long TowelCombinations(string towel)
    {
        List<string> possibleTowels = _cloths.Where(towel.Contains).ToList();
        Dictionary<string, long> cache = new();
        long combinations = RecurseTowel(towel, possibleTowels, cache);
        
        return combinations;
    }

    private static long RecurseTowel(string towel, List<string> subTowels, Dictionary<string, long> cache)
    {
        if (towel.Length == 0)
            return 1;
        if (cache.TryGetValue(towel, out long res))
            return res;

        long combinations = subTowels
            .Where(towel.StartsWith)
            .Sum(pattern => RecurseTowel(string.Concat(towel.Skip(pattern.Length)), subTowels, cache));

        cache[towel] = combinations;
        return combinations;
    }
}