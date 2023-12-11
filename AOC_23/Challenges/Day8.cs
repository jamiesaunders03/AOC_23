using System.Text.RegularExpressions;
using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day8 : IAocChallenge
    {
        internal class Navigation
        {
            /// <summary>
            /// Name of the node
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// The left hand node
            /// </summary>
            public Navigation Left { get; set; }

            /// <summary>
            /// The right hand node
            /// </summary>
            public Navigation Right { get; set; }

            public Navigation(string name, Navigation left, Navigation right)
            {
                Name = name;
                Left = left;
                Right = right;
            }

            public Navigation(string name) : this(name, null, null) { }
        }

        public int Day => 8;

        private static readonly Regex _directionRe = new(@"(\w+) = \((\w+), (\w+)\)");

        // Instructions
        private readonly string _directions;
        // Map of all paths
        private readonly Dictionary<string, Navigation> _routes;

        public Day8()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split("\n");
            _directions = input[0];
            _routes = new Dictionary<string, Navigation>();

            Match[] matches = input.Skip(2).Select(s => _directionRe.Match(s)).ToArray();

            foreach (Match m in matches)
            {
                string name = m.Groups[1].Value;
                _routes[name] = new Navigation(name);
            }

            foreach (Match m in matches)
            {
                string name = m.Groups[1].Value;
                string left = m.Groups[2].Value;
                string right = m.Groups[3].Value;

                _routes[name].Left = _routes[left];
                _routes[name].Right = _routes[right];
            }
        }

        public string Challenge1()
        {
            Navigation current = _routes["AAA"];
            int steps = 0;
            while (current.Name != "ZZZ")
            {
                int index = steps % _directions.Length;
                char direction = _directions[index];

                current = direction == 'L' ? current.Left : current.Right;
                steps++;
            }

            return steps.ToString();
        }

        public string Challenge2()
        {
            Navigation[] allNodes = _routes.Where(k => k.Key[^1] == 'A').Select(v => v.Value).ToArray();
            int[] stepsArray = new int[allNodes.Length];

            // Compute period of each route
            for (int i = 0; i < stepsArray.Length; i++)
            {
                Navigation current = allNodes[i];
                int steps = 0;
                while (current.Name[^1] != 'Z')
                {
                    int index = steps % _directions.Length;
                    char direction = _directions[index];

                    current = direction == 'L' ? current.Left : current.Right;
                    steps++;
                }

                stepsArray[i] = steps;
            }

            // Fold LCM on to array
            long arrSteps = stepsArray.Aggregate(1L, (current, num) => AocHelper.Utilities.Math.Lcm(current, num));

            return arrSteps.ToString();
        }
    }
}
