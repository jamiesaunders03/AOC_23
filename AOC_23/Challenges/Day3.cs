using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_23.Challenges
{
    internal class Day3 : IAocChallenge
    {
        internal struct PartNumber
        {
            public int Number { get; }
            public Vector2[] NumberPositions { get; }

            public PartNumber(int number, Vector2[] numberPositions)
            {
                Number = number;
                NumberPositions = numberPositions;
            }
        }

        internal struct SymbolPosition
        {
            public char Symbol { get; }
            public Vector2 Position { get; }

            public SymbolPosition(char symbol, Vector2 symbolPos)
            {
                Symbol = symbol;
                Position = symbolPos;
            }
        }

        private const char NON_SYMBOL = '.';
        private const char GEAR_SYMBOL = '*';

        private readonly PartNumber[] _parts;
        private readonly SymbolPosition[] _symbols;

        public int Day => 3;

        public Day3()
        {
            string[] input = new FetchData(3).ReadInput().TrimEnd().Split('\n');
            (_parts, _symbols) = ParseInput(input);
        }

        public string Challenge1()
        {
            int sum = 0;

            foreach (PartNumber partNum in _parts)
            {
                foreach (SymbolPosition symbol in _symbols)
                {
                    if (partNum.NumberPositions.Any(p => IsAdjacent(p, symbol.Position)))
                    {
                        sum += partNum.Number;
                        break;
                    }
                }
            }

            return sum.ToString();
        }

        public string Challenge2()
        {
            int sum = 0;

            foreach (SymbolPosition symbol in _symbols.Where(s => s.Symbol == GEAR_SYMBOL))
            {
                PartNumber[] adjacent = _parts.Where(ps => ps.NumberPositions.Any(p => IsAdjacent(p, symbol.Position))).ToArray();
                if (adjacent.Length == 2)
                {
                    sum += adjacent[0].Number * adjacent[1].Number;
                }
            }

            return sum.ToString();
        }

        public static (PartNumber[], SymbolPosition[]) ParseInput(string[] input)
        {
            var partNumbers = new List<PartNumber>();
            var symbolPos = new List<SymbolPosition>();

            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    char symbol = input[y][x];
                    if (symbol.IsNumber())
                    {
                        int num = symbol - '0';
                        List<Vector2> positions = new() { new Vector2(x, y), };

                        while (x + 1 < input[y].Length && input[y][x + 1].IsNumber())
                        {
                            x += 1;
                            num *= 10;
                            num += input[y][x] - '0';
                            positions.Add(new Vector2(x, y));
                        }

                        partNumbers.Add(new PartNumber(num, positions.ToArray()));
                    }
                    else if (symbol != NON_SYMBOL)
                    {
                        symbolPos.Add(new SymbolPosition(symbol, new Vector2(x, y)));
                    }
                }
            }

            return (partNumbers.ToArray(), symbolPos.ToArray());
        }

        private static bool IsAdjacent(Vector2 p1, Vector2 p2)
        {
            return p1.MaxDimDistance(p2) <= 1;
        }
    }
}
