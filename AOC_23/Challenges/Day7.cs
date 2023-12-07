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
            public int CardType { get; }
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
        private static  int _nCardValues = CARD_ORDER.Length;
        private static readonly Regex _cardRe = new(@"(\w+) (\d+)");
        private CardScores[] _cardScores;

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

            return sum.ToString();  // 251224871 too high
        }

        private int Compare(CardScores a, CardScores b)
        {
            if (a.Equals(b))
                return 0;

            int compare = a.CardType.CompareTo(b.CardType);
            if (compare != 0)
                return compare;

            for (int i = 0; i < a.Hand.Length; ++i)
            {
                int val1 = CARD_ORDER.IndexOf(a.Hand[i]);
                int val2 = CARD_ORDER.IndexOf(b.Hand[i]);

                compare = -val1.CompareTo(val2);
                if (compare != 0) 
                    return compare;
            }

            throw new Exception();
        }

        private int CompareWithJoker(CardScores a, CardScores b)
        {
            if (a.Equals(b))
                return 0;

            int compare = a.CardTypeWithJoker.CompareTo(b.CardTypeWithJoker);
            if (compare != 0)
                return compare;

            for (int i = 0; i < a.Hand.Length; ++i)
            {
                int val1 = CARD_ORDER_WITH_JOKER.IndexOf(a.Hand[i]);
                int val2 = CARD_ORDER_WITH_JOKER.IndexOf(b.Hand[i]);

                compare = -val1.CompareTo(val2);
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

            if (appearances[^1] == 5)
                return 7;
            else if (appearances[^1] == 4)
                return 6;
            else if (appearances[^1] == 3)
                return appearances[^2] == 2 ? 5 : 4;
            else if (appearances[^1] == 2)
                return appearances[^2] == 2 ? 3 : 2;
            else
                return 1;
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
            if (jokerIndex == 0)
            {
                max1 = appearanceCounts[1] + appearanceCounts[jokerIndex];
                max2 = appearanceCounts[2];
            }
            else if (jokerIndex == 1)
            {
                max1 = appearanceCounts[0] + appearanceCounts[jokerIndex];
                max2 = appearanceCounts[2];
            }
            else
            {
                max1 = appearanceCounts[0] + appearanceCounts[jokerIndex];
                max2 = appearanceCounts[1];
            }

            if (max1 == 5)
                return 7;
            else if (max1 == 4)
                return 6;
            else if (max1 == 3)
                return max2 == 2 ? 5 : 4;
            else if (max1 == 2)
                return max2 == 2 ? 3 : 2;
            else
                return 1;
        }
    }
}
