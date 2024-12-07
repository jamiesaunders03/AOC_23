using AocHelper;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day07 : IAocChallenge
{
    private struct Equation(long tot, long[] nums)
    {
        public long Total { get; } = tot;
        public long[] Nums { get; } = nums;
    }
    
    public int Day => 7;

    private readonly Equation[] _equations;

    public Day07()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split('\n');
        _equations = input.Select(ParseEquation).ToArray();
    }

    public string Challenge1()
    {
        var funcs = new List<Func<long, long, long>>
        {
            (first, second) => first + second,
            (first, second) => first * second,
        };

        return _equations
            .Where(e => CanProcess(e, funcs))
            .Sum(e => e.Total)
            .ToString();
    }

    public string Challenge2()
    {
        var funcs = new List<Func<long, long, long>>
        {
            (first, second) => first + second,
            (first, second) => first * second,
            (first, second) => long.Parse(first.ToString() + second.ToString()),
        };

        return _equations
            .Where(e => CanProcess(e, funcs))
            .Sum(e => e.Total)
            .ToString();
    }

    private bool CanProcess(Equation e, ICollection<Func<long, long, long>> ops)
    {
        foreach (List<Func<long, long, long>> opList in Enumeration.PermutationsFrom(ops, e.Nums.Length - 1))
        {
            long cur = e.Nums[0];
            unchecked
            {
                cur = e.Nums
                    .Skip(1)
                    .Zip(opList)
                    .Aggregate(cur, (current, iterOp) => iterOp.Second.Invoke(current, iterOp.First));
            }

            if (cur == e.Total)
                return true;
        }

        return false;
    }

    private static Equation ParseEquation(string line)
    {
        string[] parts = line.Split(": ");
        long[] nums = parts[1].Split(' ').Select(long.Parse).ToArray();

        return new Equation(long.Parse(parts[0]), nums);
    }
}