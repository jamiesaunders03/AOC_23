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

        private string _directions;

        public Day8()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split("\n");
            _directions = input[0];
        }

        public string Challenge1()
        {
            throw new NotImplementedException();
        }

        public string Challenge2()
        {
            throw new NotImplementedException();
        }
    }
}
