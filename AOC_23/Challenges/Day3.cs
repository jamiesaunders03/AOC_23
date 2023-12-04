using AocHelper;

using Position = System.Tuple<int, int>;

namespace AOC_23.Challenges
{
    internal class Day3 : IAocChallenge
    {
        internal struct PartNumber
        {
            public int Number { get; }
            public Position[] NumberPositions { get; }

            public PartNumber(int number, Position[] numberPositions)
            {
                Number = number;
                NumberPositions = numberPositions;
            }
        }

        internal struct SymbolPosition
        {
            public char Symbol { get; }
            public Position Position { get; }

            public SymbolPosition(char symbol, Position symbolPos)
            {
                Symbol = symbol;
                Position = symbolPos;
            }
        }

        private const char NON_SYMBOL = '.';
        private const char GEAR_SYMBOL = '*';

        public void RunChallenge()
        {
            string[] input = new FetchData(3).ReadInput().TrimEnd().Split('\n');
            (PartNumber[] parts, SymbolPosition[] symbols) = ParseInput(input);

            int part1 = Challenge1(parts, symbols);
            int part2 = Challenge2(parts, symbols);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        public int Challenge1(PartNumber[] partNumbers, SymbolPosition[] symbolPositions)
        {
            int sum = 0;

            foreach (PartNumber partNum in partNumbers)
            {
                foreach (SymbolPosition symbol in symbolPositions)
                {
                    if (partNum.NumberPositions.Any(p => Close(p, symbol.Position)))
                    {
                        sum += partNum.Number;
                        break;
                    }
                }
            }

            return sum;
        }

        public int Challenge2(PartNumber[] partNumbers, SymbolPosition[] symbolPositions)
        {
            int sum = 0;

            foreach (SymbolPosition symbol in symbolPositions.Where(s => s.Symbol == GEAR_SYMBOL))
            {
                PartNumber[] adjacent = partNumbers.Where(ps => ps.NumberPositions.Any(p => Close(p, symbol.Position))).ToArray();
                if (adjacent.Length == 2)
                {
                    sum += adjacent[0].Number * adjacent[1].Number;
                }
            }

            return sum;
        }

        public (PartNumber[], SymbolPosition[]) ParseInput(string[] input)
        {
            var partNumbers = new List<PartNumber>();
            var symbolPos = new List<SymbolPosition>();

            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    char symbol = input[y][x];
                    if (IsNumber(symbol))
                    {
                        int num = symbol - '0';
                        List<Position> positions = new() { new Position(x, y), };

                        while (x + 1 < input[y].Length && IsNumber(input[y][x + 1]))
                        {
                            ++x;
                            num *= 10;
                            num += input[y][x] - '0';
                            positions.Add(new Position(x, y));
                        }

                        partNumbers.Add(new PartNumber(num, positions.ToArray()));
                    }
                    else if (symbol != NON_SYMBOL)
                    {
                        symbolPos.Add(new SymbolPosition(symbol, new Position(x, y)));
                    }
                }
            }

            return (partNumbers.ToArray(), symbolPos.ToArray());
        }

        private static bool IsNumber(char character)
        {
            return character is >= '0' and <= '9';
        }

        private bool Close(Position p1, Position p2)
        {
            return Math.Abs(p1.Item1 - p2.Item1) <= 1 && Math.Abs(p1.Item2 - p2.Item2) <= 1;
        }
    }
}
