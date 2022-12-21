namespace BlackJack
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>();
        public List<Card> UsedCards = new List<Card>();

        public Deck(string[] _suits, string[] _values )
        {
            //cyklus, který běží po jednom suitz
            foreach(string suit in _suits)
            {
                //ke každýmu suitu se přidají všechny values
                foreach(string value in _values)
                {
                    Cards.Add(new Card(value, suit));
                }
            }
        }

        public List<string> Shuffle(List<string> card)
        {
            bool ok = true;
            int counter = 0;
            var random = new Random();
            int num = random.Next(0, Cards.Count);

            string value = Cards[num].Value;
            string suit = Cards[num].Suit;

            UsedCards.Add(new Card(value, suit));

            //loop
            while (ok)
            {
                //cyklus, aby proběhl v rozmezí kolik je použitých karet
                for (int i = 0; i < UsedCards.Count; i++)
                {
                    //pokud je karta ve využitých kartách, tak vytvoří novou
                    if (UsedCards[i].Suit == suit && UsedCards[i].Value == value)
                    {
                        random = new Random();
                        num = random.Next(0, Cards.Count);

                        value = Cards[num].Value;
                        suit = Cards[num].Suit;
                        counter++;
                    }
                }

                //pokud se nenašel duplikát
                if (counter == 0)
                {
                    ok = false;
                }
                counter = 0;
            }

            card.Add(value);

            Console.Write(value + suit + "\n");

            return card;
        }

        public int AddToTotal(List<string> hand, int total, int number_of_cards)
        {
            //pokud přidaná karta není číslo
            if (!int.TryParse(hand[number_of_cards], out int result))
            {
                //pokud se nerovná karta A
                if (hand[number_of_cards] != "A")
                {
                    result = 10;
                }
                //pokud ne
                else
                {
                    //rozlišení jestli se má přidat hráčí 1 nebo 11
                    if (total > 10)
                    {
                        result = 1;
                    }
                    //pokud má hráč méně nebo rovno 10
                    else
                    {
                        result = 11;
                    }
                }
            }
            return result;
        }
    }
}
