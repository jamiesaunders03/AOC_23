using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day6 : IAocChallenge
    {
        public int Day => 6;

        private readonly string[] _times;
        private readonly string[] _dists;

        public Day6()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
            _times = input[0].Split().Skip(1).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            _dists = input[1].Split().Skip(1).Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }

        public string Challenge1()
        {
            int[] times = _times.Select(int.Parse).ToArray();
            int[] dists = _dists.Select(int.Parse).ToArray();
            long[] tolerance = new long[times.Length];

            for (int i = 0; i < times.Length; ++i)
            {
                long min = QuadFormulaMin(times[i], dists[i]);
                tolerance[i] = times[i] - 2 * min + 1;
            }

            long prod = tolerance[0];
            foreach (long l in tolerance.Skip(1))
            {
                prod *= l;
            }

            return prod.ToString();
        }

        public string Challenge2()
        {
            long time = long.Parse(string.Concat(_times));
            long dist = long.Parse(string.Concat(_dists));

            long min = QuadFormulaMin(time, dist);
            long tolerance = time - 2 * min + 1;
            return tolerance.ToString();
        }

        private static long QuadFormulaMin(long time, long dist)
        {
            /*
             * a + b = time
             * a * b = dist
             * b = time - a
             * a * (time - a) = dist
             *
             * -a^2 + a*time = dist
             * a^2 - time*a + dist = 0
             * (-b +/-sqrt(b^2 - 4ac)) / 2a
             *
             * a = 1, b = time, c = dist
             * (-time +/-sqrt(time^2-4*dist)) / 2
             *
             * For min, take neg of sqrt
             * (-time - sqrt(time^2-4*dist)) / 2
             * And round up
             */
            double min = (time - Math.Sqrt(Math.Pow(time, 2) - 4 * dist)) / 2;
            return (long)Math.Ceiling(min);
        }
    }
}
