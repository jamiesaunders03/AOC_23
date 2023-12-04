using AocHelper;

using System.Text.RegularExpressions;

namespace AOC_23.Challenges
{
    internal class Day2 : IAocChallenge
    {
        internal struct Game
        {
            /// <summary>
            /// The game ID
            /// </summary>
            public int Id { get; }

            /// <summary>
            /// The selection of colours for this game
            /// </summary>
            public int[,] Colours { get; }

            public Game(int id, int[,] colours)
            {
                Id = id;
                Colours = colours;
            }
        }

        private const int N_COLOURS = 3;
        private static readonly int[] _maxValues = { 12, 13, 14, };

        private static readonly Regex _gameRegex = new(@"Game (\d+): (.*)");
        private static readonly Regex _numColRegex = new(@"(\d+) (\w+)");

        private static readonly Dictionary<string, int> _colPosition = new()
        {
            ["red"] = 0,
            ["green"] = 1,
            ["blue"] = 2,
        };

        private readonly Game[] _games;

        public int Day => 2;

        public Day2()
        {
            string[] input = new FetchData(2).ReadInput().TrimEnd().Split('\n');
            _games = ParseGame(input);
        }

        public string Challenge1()
        {
            int sum = 0;
            foreach (Game g in _games)
            {
                bool isValidGame = true;
                for (int group = 0; group < g.Colours.GetLength(0); ++group)
                {
                    for (int i = 0; i < N_COLOURS; ++i)
                    {
                        if (g.Colours[group, i] > _maxValues[i])
                        {
                            isValidGame = false;
                            break;
                        }
                    }
                }

                if (isValidGame)
                    sum += g.Id;
            }

            return sum.ToString();
        }

        public string Challenge2()
        {
            int prod = 0;
            foreach (Game g in _games)
            {
                int[] mins = new int[3];
                for (int group = 0; group < g.Colours.GetLength(0); ++group)
                {
                    for (int i = 0; i < N_COLOURS; ++i)
                    {
                        if (g.Colours[group, i] > mins[i] )
                        {
                            mins[i] = g.Colours[group, i];
                        }
                    }
                }

                prod += mins[0] * mins[1] * mins[2];
            }

            return prod.ToString();
        }

        private Game[] ParseGame(string[] input)
        {
            var games = new Game[input.Length];
            for (int i = 0; i < games.Length; ++i)
            {
                Match match = _gameRegex.Match(input[i].TrimEnd());
                int id = int.Parse(match.Groups[1].Value);
                string rest = match.Groups[2].Value;
                int[,] colours = ParseColours(rest);

                games[i] = new Game(id, colours);
            }

            return games;
        }

        private static int[,] ParseColours(string row)
        {
            string[] groups = row.Split(';');
            int[,] colours = new int[groups.Length, 3];

            for (int g = 0; g < groups.Length; ++g)
            {
                string[] cols = groups[g].Split(", ");
                foreach (string colour in cols)
                {
                    Match colMatch = _numColRegex.Match(colour);
                    int index = _colPosition[colMatch.Groups[2].Value];
                    colours[g, index] = int.Parse(colMatch.Groups[1].Value);
                }
            }

            return colours;
        }
    }
}
