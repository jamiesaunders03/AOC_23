using System.Text.RegularExpressions;
using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day4 : IAocChallenge
    {
        internal struct ScratchCard
        {
            public int CardNumber { get; }
            public HashSet<int> WinningNumbers { get; }
            public HashSet<int> CardNumbers { get; }

            public ScratchCard(int cardNumber, HashSet<int> winningNumbers, HashSet<int> cardNumbers)
            {
                CardNumber = cardNumber;
                WinningNumbers = winningNumbers;
                CardNumbers = cardNumbers;
            }
        }

        public static readonly Regex _cardRegex = new(@"Card\s+(\d+): ([0-9 ]+) \| ([0-9 ]+)");

        private readonly ScratchCard[] _cards;

        public int Day => 4;

        public Day4()
        {
            string[] input = new FetchData(4).ReadInput().TrimEnd().Split('\n');
            _cards = ParseInput(input);
        }

        public string Challenge1()
        {
            int sum = 0;

            foreach (ScratchCard card in _cards)
            {
                int overlap = card.CardNumbers.Intersect(card.WinningNumbers).Count();
                if (overlap > 0)
                    sum += 1 << (overlap - 1);
            }

            return sum.ToString();
        }

        public string Challenge2()
        {
            int[] cardCounts = Enumerable.Repeat<int>(1, _cards.Length).ToArray();

            for (int i = 0; i < _cards.Length; ++i)
            {
                ScratchCard card = _cards[i];
                int overlap = card.CardNumbers.Intersect(card.WinningNumbers).Count();

                for (int cardNum = card.CardNumber; cardNum < card.CardNumber + overlap; ++cardNum)
                {
                    cardCounts[cardNum] += cardCounts[i];
                }
            }

            return cardCounts.Sum().ToString();
        }

        private ScratchCard[] ParseInput(string[] input)
        {
            var cards = new ScratchCard[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                Match m = _cardRegex.Match(input[i]);
                int cardNum = int.Parse(m.Groups[1].Value);
                int[] winningNums = GetArrayFromIntString(m.Groups[2].Value);
                int[] ourNums = GetArrayFromIntString(m.Groups[3].Value);

                cards[i] = new ScratchCard(cardNum, new HashSet<int>(winningNums), new HashSet<int>(ourNums));
            }

            return cards;
        }

        private static int[] GetArrayFromIntString(string nums)
        {
            // Remove double space, then split on single space
            nums = nums.Replace("  ", " ").Trim();
            string[] numGroups = nums.Split(' ');

            return numGroups.Select(int.Parse).ToArray();
        }
    }
}
