using AocHelper;
using AocHelper.Utilities;

namespace AOC_23.Challenges
{
    internal class Day11 : IAocChallenge
    {
        public int Day => 11;

        private const char GALAXY = '#';
        private readonly string[] _space;

        public Day11()
        {
            _space = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
        }

        public string Challenge1()
        {
            List<Vector2> galaxies = GetGalaxies(_space, 2);
            long sum = SumGalaxyDistances(galaxies);

            return sum.ToString();
        }

        public string Challenge2()
        {
            List<Vector2> galaxies = GetGalaxies(_space, 1_000_000);
            long sum = SumGalaxyDistances(galaxies);

            return sum.ToString();
        }

        private static List<Vector2> GetGalaxies(string[] space, int sep)
        {
            List<Vector2> galaxies = new();

            List<int> emptyRows = new();
            List<int> emptyCols = new();
            for (int i = 0; i < space.Length; i++)
            {
                if (space[i].All(c => c != GALAXY))
                    emptyRows.Add(i);
            }

            for (int j = 0; j < space[0].Length; j++)
            {
                if (space.All(r => r[j] != GALAXY))
                    emptyCols.Add(j);
            }

            foreach (((int i, int j), char c) in Enumeration.EnumerateArray(space))
            {
                if (c == GALAXY)
                {
                    int iOffset = emptyRows.Count(v => i >= v) * (sep - 1);
                    int jOffset = emptyCols.Count(v => j >= v) * (sep - 1);

                    galaxies.Add(new Vector2(j + jOffset, i + iOffset));
                }
            }

            return galaxies;
        }

        private static long SumGalaxyDistances(List<Vector2> galaxies)
        {
            long sum = 0;

            for (int i = 0; i < galaxies.Count - 1; ++i)
                for (int j = i + 1; j < galaxies.Count; ++j)
                    sum += galaxies[i].Manhattan(galaxies[j]);

            return sum;
        }
    }
}
