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

            public CardScores(string hand, int value)
            {
                Hand = hand;
                Value = value;
                CardType = GetCardType(hand);
            }
        }

        public int Day => 7;

        private const string CARD_ORDER = "AKQJT98765432";
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
            throw new NotImplementedException();
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
    }
}
