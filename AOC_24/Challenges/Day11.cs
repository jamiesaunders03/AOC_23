using AocHelper;
using AocHelper.DataStructures;

namespace AOC_24.Challenges;

internal class Day11 : IAocChallenge
{
    public int Day => 11;

    private readonly DefaultDictionary<long, long> _stones;

    public Day11()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split(' ');

        _stones = new DefaultDictionary<long, long>();
        foreach (int num in input.Select(int.Parse))
        {
            _stones[num] += 1;
        }
    }

    public string Challenge1()
    {
        Dictionary<long, long> stones = Blink(_stones, 25);
        return stones.Values.Sum().ToString();
    }

    public string Challenge2()
    {
        Dictionary<long, long> stones = Blink(_stones, 75);
        return stones.Values.Sum().ToString();
    }

    /// <summary>
    /// Blinks the stones the given number of times by the rules:
    /// - A stone marked 0, becomes 1
    /// - A stone with an even number of digits gets split in half
    /// - Any other stones get multiplied by 2024
    /// </summary>
    /// <param name="stones">The inital stone arangement</param>
    /// <param name="nTimes">The number of times to blink</param>
    /// <returns></returns>
    private Dictionary<long, long> Blink(DefaultDictionary<long, long> stones, long nTimes)
    {
        for (int _ = 0; _ < nTimes; ++_)
        {
            DefaultDictionary<long, long> nextStones = new();

            foreach ((long key, long val) in stones)
            {
                BlinkStone(nextStones, key, val);
            }

            stones = nextStones;
        }
        
        return stones;
    }

    /// <summary>
    /// Determines how an individual stone blinks in each frame, and adds the resulting stones to the collection
    /// </summary>
    /// <param name="stones">The collection of stones to add to</param>
    /// <param name="stoneNum">The number on the current stone</param>
    /// <param name="nStones">The count of stones with this number</param>
    private static void BlinkStone(DefaultDictionary<long, long> stones, long stoneNum, long nStones)
    {
        if (stoneNum == 0)
        {
            stones[1] += nStones;
        }
        else if (stoneNum.ToString().Length % 2 == 0)
        {
            string stoneStr = stoneNum.ToString();
            string firstNum = stoneStr[..(stoneStr.Length / 2)];
            string secondNum = stoneStr[(stoneStr.Length / 2)..];

            stones[long.Parse(firstNum)] += nStones;
            stones[long.Parse(secondNum)] += nStones;
        }
        else
        {
            stones[stoneNum * 2024] += nStones;
        }
    }
}