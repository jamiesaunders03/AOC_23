using System.Text.RegularExpressions;
using AocHelper;

namespace AOC_24.Challenges;

internal partial class Day03 : IAocChallenge
{
    public int Day => 3;

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();
    private static readonly Regex _mulRe = MulRegex();
    
    [GeneratedRegex(@"mul\((\d+),(\d+)\)|don't|do")]
    private static partial Regex ConditionalMulRegex();
    private static readonly Regex _condMulRe = ConditionalMulRegex();

    private readonly string _sequence;

    public Day03()
    {
        _sequence = new FetchData(Day, 2024).ReadInput().TrimEnd();
    }

    public string Challenge1()
    {
        int total = 0;

        MatchCollection matchCol = _mulRe.Matches(_sequence);
        foreach (Match m in matchCol)
        {
            int first = int.Parse(m.Groups[1].Value);
            int second = int.Parse(m.Groups[2].Value);

            total += first * second;
        }

        return total.ToString();
    }

    public string Challenge2()
    {
        int total = 0;
        bool do_ = true;

        MatchCollection matchCol = _condMulRe.Matches(_sequence);
        foreach (Match m in matchCol)
        {
            switch (m.Groups[0].Value)
            {
                case "don't":
                    do_ = false;
                    break;
                case "do":
                    do_ = true;
                    break;
                case not null when do_:
                    int first = int.Parse(m.Groups[1].Value);
                    int second = int.Parse(m.Groups[2].Value);
                    total += first * second;
                    break;
            }
        }

        return total.ToString();
    }
}