using System.Text.RegularExpressions;

using AocHelper;

namespace AOC_23.Challenges
{
    internal class Day7 : IAocChallenge
    {
        internal struct CardScores
        {
            public string Hand { get; }
            public int Value { get; }

            // Storage of cart type
            public int CardType { get; }
            // Storage of card type when 'J' is the joker
            public int CardTypeWithJoker { get; }

            public CardScores(string hand, int value)
            {
                Hand = hand;
                Value = value;
                CardType = GetCardType(hand);
                CardTypeWithJoker = GetCardTypeWithJoker(hand);
            }
        }

        public int Day => 7;

        private const string CARD_ORDER = "AKQJT98765432";
        private const string CARD_ORDER_WITH_JOKER = "AKQT98765432J";
        private static readonly int _nCardValues = CARD_ORDER.Length;
        private static readonly Regex _cardRe = new(@"(\w+) (\d+)");
        private readonly CardScores[] _cardScores;

        public Day7()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split("\n");
            _cardScores = new CardScores[input.Length];

            for (int i = 0; i < input.Length; ++i)
            {
                Match m = _cardRe.Match(input[i]);
                string card = m.Groups[1].Value;
                int score = int.Parse(m.Groups[2].Value);

                _cardScores[i] = new CardScores(card, score);
            }
        }

        public string Challenge1()
        {
            List<CardScores> cards = new(_cardScores);
            cards.Sort(Compare);

            int multiplier = 1;
            long sum = 0;
            foreach (CardScores card in cards)
            {
                sum += card.Value * multiplier;
                ++multiplier;
            }

            return sum.ToString();
        }

        public string Challenge2()
        {
            List<CardScores> cards = new(_cardScores);
            cards.Sort(CompareWithJoker);

            int multiplier = 1;
            long sum = 0;
            foreach (CardScores card in cards)
            {
                sum += card.Value * multiplier;
                ++multiplier;
            }

            return sum.ToString();
        }

        private int Compare(CardScores a, CardScores b)
        {
            if (a.Equals(b))
                return 0;

            int compare = a.CardType.CompareTo(b.CardType);
            if (compare != 0)
                return compare;

            return CompareStringsWithMapping(a.Hand, b.Hand, CARD_ORDER);
        }

        private int CompareWithJoker(CardScores a, CardScores b)
        {
            if (a.Equals(b))
                return 0;

            int compare = a.CardTypeWithJoker.CompareTo(b.CardTypeWithJoker);
            if (compare != 0)
                return compare;

            return CompareStringsWithMapping(a.Hand, b.Hand, CARD_ORDER_WITH_JOKER);
        }

        private static int CompareStringsWithMapping(string a, string b, string mapping)
        {
            for (int i = 0; i < a.Length; ++i)
            {
                int val1 = mapping.IndexOf(a[i]);
                int val2 = mapping.IndexOf(b[i]);

                int compare = -val1.CompareTo(val2);
                if (compare != 0)
                    return compare;
            }

            throw new Exception();
        }

        private static int GetCardType(string card)
        {
            List<int> appearances = new List<int>(new int[_nCardValues]);
            foreach (char c in card)
                appearances[CARD_ORDER.IndexOf(c)] += 1;

            appearances.Sort();

            return GetCardTypeFromMaxes(appearances[^1], appearances[^2]);
        }

        private static int GetCardTypeWithJoker(string card)
        {
            var appearances = CARD_ORDER_WITH_JOKER.ToDictionary(k => k, k => 0);
            foreach (char c in card)
                appearances[c] += 1;

            List<Tuple<char, int>> appearanceArray = appearances.Select(i => new Tuple<char, int>(i.Key, i.Value)).ToList();
            appearanceArray.Sort((i1, i2) => -i1.Item2.CompareTo(i2.Item2));

            int jokerIndex = appearanceArray.Select(i => i.Item1).ToList().IndexOf('J');
            int[] appearanceCounts = appearanceArray.Select(t => t.Item2).ToArray();

            int max1;
            int max2;
            switch (jokerIndex)
            {
                case 0:
                    max1 = appearanceCounts[1] + appearanceCounts[jokerIndex];
                    max2 = appearanceCounts[2];
                    break;
                case 1:
                    max1 = appearanceCounts[0] + appearanceCounts[jokerIndex];
                    max2 = appearanceCounts[2];
                    break;
                default:
                    max1 = appearanceCounts[0] + appearanceCounts[jokerIndex];
                    max2 = appearanceCounts[1];
                    break;
            }

            return GetCardTypeFromMaxes(max1, max2);
        }

        private static int GetCardTypeFromMaxes(int max, int secondMax)
        {
            return (new[] { max, secondMax }) switch
            {
                [5, _] => 7,
                [4, _] => 6,
                [3, 2] => 5,
                [3, _] => 4,
                [2, 2] => 3,
                [2, _] => 2,
                _ => 1,
            };
        }
    }
}
