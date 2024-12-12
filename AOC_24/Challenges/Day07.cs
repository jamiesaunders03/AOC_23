using System.Configuration;
using AocHelper;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day07 : IAocChallenge
{
    private readonly struct Equation(long tot, long[] nums)
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
            Add,
            Mul,
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
            Add,
            Mul,
            ConCat,
        };

        return _equations
            .Where(e => CanProcess(e, funcs))
            .Sum(e => e.Total)
            .ToString();
    }

    private bool CanProcess(Equation e, ICollection<Func<long, long, long>> ops)
    {
        if (!BasicCheckFast(e, ops))
        {
            return false;
        }
        
        foreach (List<Func<long, long, long>> opList in Enumeration.PermutationsFrom(ops, e.Nums.Length - 1))
        {
            long cur = e.Nums[0];
            try
            {
                cur = e.Nums
                    .Skip(1)
                    .Zip(opList)
                    .Aggregate(cur, (current, iterOp) => iterOp.Second.Invoke(current, iterOp.First));
                
                if (cur == e.Total)
                    return true;
            }
            catch (OverflowException) { }
        }

        return false;
    }

    /// <summary>
    /// Returns false if it impossible for the given input to match the target.
    /// Returns true if there may be an input that makes it possible.
    /// This is calculated with bounds checking the input against potential min / max values
    /// </summary>
    private static bool BasicCheckFast(Equation e, ICollection<Func<long, long, long>> ops)
    {
        return e.Nums.Sum() - e.Nums.Count(n => n == 1) <= e.Total;
    }

    private static Equation ParseEquation(string line)
    {
        string[] parts = line.Split(": ");
        long[] nums = parts[1].Split(' ').Select(long.Parse).ToArray();

        return new Equation(long.Parse(parts[0]), nums);
    }

    private static long Add(long first, long second)
    {
        return first + second;
    }

    private static long Mul(long first, long second)
    {
        return first * second;
    }

    private static long ConCat(long first, long second)
    {
        return second switch
        {
            < 10 => 10L * first + second,
            < 100 => 100L * first + second,
            < 1000 => 1000L * first + second,
            < 10000 => 10000L * first + second,
            < 100000 => 100000L * first + second,
            < 1000000 => 1000000L * first + second,
            < 10000000 => 10000000L * first + second,
            < 100000000 => 100000000L * first + second,
            _ => 1000000000L * first + second,
        };
    }
}