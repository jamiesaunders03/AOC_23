using System.Text.RegularExpressions;
using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;
using Math = System.Math;

namespace AOC_24.Challenges;

internal partial class Day17 : IAocChallenge
{
    private const int A = 0;
    private const int B = 1;
    private const int C = 2;
    
    [GeneratedRegex(@"Register \w: (\d+)")]
    private static partial Regex RegisterRe();
    private static readonly Regex _regRe = RegisterRe();

    private static readonly Dictionary<long, Func<long, List<long>, int, List<long>, int>> _opcodeMap = new()
    {
        [0] = Adv,
        [1] = Bxl,
        [2] = Bst,
        [3] = Jnz,
        [4] = Bxc,
        [5] = Out,
        [6] = Bdv,
        [7] = Cdv,
    };
    
    public int Day => 17;

    private readonly List<long> _registers;
    private readonly List<long> _program;

    public Day17()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split("\n\n");
        // string[] input = File.ReadAllText("../../../TestCases/day17.txt").TrimEnd().Split("\r\n\r\n");
        string[] regs = input[0].Split("\n");
        
        _registers = new List<long>();
        for (int i = 0; i < 3; ++i)
        {
            Match m = _regRe.Match(regs[i]);
            _registers.Add(int.Parse(m.Groups[1].Value));
        }

        _program = input[1]
            .Split(' ')[1]
            .Split(',')
            .Select(long.Parse)
            .ToList();
    }

    public string Challenge1()
    {
        List<long> registers = _registers.ToList();
        List<long> outBuf = Run(registers);

        return string.Join(',', outBuf);
    }

    public string Challenge2()
    {
        return "";
        
        HashSet<long> solutions = BackTrack();
        long min = solutions.Min();

        return min.ToString();
    }

    private static long GetComboOperand(long num, List<long> regs)
    {
        return num <= 3 ? num : regs[(int)num - 4];
    }

    private List<long> Run(List<long> registers)
    {
        List<long> outBuf = [];
        int ptr = 0;

        while (0 <= ptr && ptr < _program.Count - 1)
        {
            ptr = _opcodeMap[_program[ptr]]((int)_program[ptr + 1], registers, ptr, outBuf);
        }

        return outBuf;
    }

    private HashSet<long> BackTrack()
    {
        /*
         * B = A % 8
         * B = B ^ 3
         * C = A / (2^B)
         * B = B ^ 5
         * A = A / 8
         * B = B ^ C
         * PRINT B % 8
         * IF A != 0, GOTO START
         */
        
        return [];
    }

    #region Operations

    private static int Adv(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long divPower = GetComboOperand(val, regs);
        long newVal = regs[A] / (int)Math.Pow(2, divPower);
        regs[A] = newVal;

        return ptr + 2;
    }
    
    private static int Bxl(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long xor = val ^ regs[B];
        regs[B] = xor;

        return ptr + 2;
    }
    
    private static int Bst(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long mod = GetComboOperand(val, regs) % 8;
        regs[B] = mod;

        return ptr + 2;
    }
    
    private static int Jnz(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        if (regs[A] == 0)
            return ptr + 2;

        return (int)val;
    }

    private static int Bxc(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long newVal = regs[B] ^ regs[C];
        regs[B] = newVal;

        return ptr + 2;
    }
    
    private static int Out(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long mod = GetComboOperand(val, regs) % 8;
        outBuf.Add(mod);

        return ptr + 2;
    }
    
    private static int Bdv(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long divPower = GetComboOperand(val, regs);
        long newVal = regs[A] / (int)Math.Pow(2, divPower);
        regs[B] = newVal;

        return ptr + 2;
    }
    
    private static int Cdv(long val, List<long> regs, int ptr, List<long> outBuf)
    {
        long divPower = GetComboOperand(val, regs);
        long newVal = regs[A] / (int)Math.Pow(2, divPower);
        regs[C] = newVal;

        return ptr + 2;
    }

    #endregion
}