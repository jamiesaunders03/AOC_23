using AocHelper;
using AocHelper.Utilities;
using Math = System.Math;

namespace AOC_23.Challenges
{
    internal class Day13 : IAocChallenge
    {
        public int Day => 13;

        private const int HORIZONTAL_MULTIPLIER = 100;
        private readonly string[][] _grids;

        public Day13()
        {
            _grids = new FetchData(Day).ReadInput().TrimEnd()
                .Split("\n\n")
                .Select(s => s.Split('\n'))
                .ToArray();
        }

        public string Challenge1()
        {
            int sum = _grids.Sum(GetGridScore);
            return sum.ToString();
        }

        public string Challenge2()
        {
            int sum = 0;
            foreach (string[] grid in _grids)
            {
                char[][] gridParts = grid.Select(s => s.ToCharArray()).ToArray();
                sum += RunPermutations(gridParts);
            }

            return sum.ToString();  // 36178 too low
        }

        private int GetGridScore(string[] grid)
        {
            int score = GetScore(grid.Select(c => c.ToCharArray()).ToArray());
            if (score != -1)
                return score * HORIZONTAL_MULTIPLIER;

            char[][] rotated = new char[grid[0].Length][];
            for (int i = 0; i < grid[0].Length; i++)
            {
                rotated[i] = grid.Select(s => s[i]).ToArray();
            }

            score = GetScore(rotated);
            return score;
        }

        private int GetScore(char[][] grid)
        {
            for (int i = 1; i < grid.Length; i++)
            {
                char[][] start = grid[..i];
                char[][] end = grid[i..];

                int r = Math.Min(start.Length, end.Length);
                bool equal = true;
                for (int j = 0; j < r; j++)
                {
                    if (!start[^(j+1)].SequenceEqual(end[j]))
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal)
                    return i;
            }

            return -1;
        }

        private int RunPermutations(char[][] grid)
        {
            for (int i = 0; i < grid.Length; ++i)
            {
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    // Discount non-changed versions
                    char current = grid[i][j];
                    grid[i][j] = current == '#' ? '.' : '#';
                    int score = GetGridScore(grid.Select(s => new string(s)).ToArray());
                    grid[i][j] = current;

                    if (score != -1)
                        return score;
                }
            }

            throw new Exception();
        }
    }
}
