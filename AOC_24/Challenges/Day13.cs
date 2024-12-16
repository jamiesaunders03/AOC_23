using System.Text.RegularExpressions;
using AocHelper;
using AocHelper.DataStructures;

namespace AOC_24.Challenges;

internal partial class Day13 : IAocChallenge
{
    private const int A_COST = 3;
    private const int B_COST = 1;
    private const long SHIFT = 10_000_000_000_000;
    
    [GeneratedRegex(@"X.(\d+), Y.(\d+)")]
    private static partial Regex LocationRegex();
    private static readonly Regex _locationRegex = LocationRegex();
    
    public int Day => 13;

    /// <summary>
    /// The inputs and prize location for the given machine
    /// </summary>
    private struct Machine
    {
        public Vector2 ButtonA { get; init; }
        public Vector2 ButtonB { get; init; }
        public Vector2 Prize { get; init; }
    }

    private readonly ICollection<Machine> _machines;

    public Day13()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split("\n\n");
        
        _machines = [];
        foreach (string machine in input)
        {
            MatchCollection matches = _locationRegex.Matches(machine);
            Vector2[] vecs = matches
                                .Select(Vector2.FromMatch)
                                .ToArray();

            _machines.Add(new Machine { ButtonA = vecs[0], ButtonB = vecs[1], Prize = vecs[2] });
        }
    }

    public string Challenge1()
    {
        long tokens = _machines
            .Select(m => GetTokensToWinPrize(m, out int toks) ? toks : 0)
            .Sum();

        return tokens.ToString();
    }

    public string Challenge2()
    {
        long tokens = _machines
            .Select(m => GetTokensToWinFarPrize(m, out long toks) ? toks : 0)
            .Sum();

        return tokens.ToString();
    }

    /// <summary>
    /// Gets the number of tokens required to win the prize, if possible.
    /// Returns true if the prize can be won.
    /// </summary>
    /// <param name="m">The machine to try</param>
    /// <param name="tokens">The number of tokens required to win</param>
    /// <returns></returns>
    private static bool GetTokensToWinPrize(Machine m, out int tokens)
    {
        tokens = int.MaxValue;

        for (int a = 0; a < 100; a++)
        {
            for (int b = 0; b < 100; ++b)
            {
                Vector2 pos = m.ButtonA * a + m.ButtonB * b;
                if (pos == m.Prize)
                {
                    int cost = a * A_COST + b * B_COST;
                    tokens = Math.Min(tokens, cost);
                }

                if (pos.X >= m.Prize.X || pos.Y >= m.Prize.Y)
                {
                    break;
                }
            }
        }
        
        return tokens != int.MaxValue;
    }

    /// <summary>
    /// Gets the number of tokens required to win the far prize, if possible.
    /// Returns true if the prize can be won.
    /// </summary>
    /// <param name="m">The machine to try</param>
    /// <param name="tokens">The number of tokens required to win</param>
    /// <returns></returns>
    private static bool GetTokensToWinFarPrize(Machine m, out long tokens)
    {
        tokens = int.MaxValue;
        m = m with { Prize = m.Prize + new Vector2(SHIFT, SHIFT) };
        var eqMat = new Matrix(new double[,]
        {
            { m.ButtonA.X, m.ButtonB.X },
            { m.ButtonA.Y, m.ButtonB.Y },
        });
        var prizeMat = new Matrix(new double[,]
        {
            { m.Prize.X }, 
            { m.Prize.Y },
        });

        Matrix result = eqMat.Inverse().MatMul(prizeMat);

        long x = (long)Math.Round(result[0, 0]);
        long y = (long)Math.Round(result[1, 0]);

        Vector2 predictedPos = m.ButtonA * x + m.ButtonB * y;
        if (predictedPos == m.Prize)
        {
            tokens = x * A_COST + y * B_COST;
        }
        
        return tokens != int.MaxValue;
    }
}